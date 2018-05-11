using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder {

    private static Pathfinder m_instance;
    private static PathNode[] m_allNodes;
    public static Pathfinder Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new Pathfinder();
            }

            return m_instance;
        }
    }

    public static bool showAll = false;

    public static void ConnectNodes(LayerMask blockers)
    {
        Debug.Log("Starting node scan & connect");

        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Pathnode");

        List<PathNode> nodeBuffer = new List<PathNode>();
        List<PathNode> nodeStore = new List<PathNode>();

        for (int n = 0; n < allNodes.Length; n++)
        {
            nodeBuffer.Clear();

            for (int o = 0; o < allNodes.Length; o++)
            {
                if (n == o) continue;
                if (Vector3.Distance(allNodes[n].transform.position, allNodes[o].transform.position) > 15f) continue;
                if(Physics2D.Linecast(allNodes[n].transform.position, allNodes[o].transform.position).collider == null)
                {
                    nodeBuffer.Add(allNodes[o].GetComponent<PathNode>());
                }
            }

            PathNode currentNode = allNodes[n].GetComponent<PathNode>();
            currentNode.connectedNodes = nodeBuffer.ToArray();
            nodeStore.Add(currentNode);
        }

        m_allNodes = nodeStore.ToArray();
        Debug.Log("Finished, total nodes: " + m_allNodes.Length);
    }

    public Vector3 GetNextPosition(Vector3 targetPosition, Vector3 currentPosition)
    {
        if(m_allNodes == null)
        {
            //GameObject[] allGOs = GameObject.FindGameObjectsWithTag("Pathnode");
            //m_allNodes = new PathNode[allGOs.Length];

            //for (int i = 0; i < allGOs.Length; i++)
            //{
            //    m_allNodes[i] = allGOs[i].GetComponent<PathNode>();
            //}

            ConnectNodes(1 << LayerMask.NameToLayer("Terrain"));
        }

        

        PathNode sourceNode = GetClosestNode(currentPosition);
        PathNode targetNode = GetClosestNode(targetPosition);

        //Reset node values

        for (int i = 0; i < m_allNodes.Length; i++)
        {
            m_allNodes[i].m_visited = false;
            m_allNodes[i].m_totalValue = 9999f;
            m_allNodes[i].displayColor = Color.yellow;
        }

        targetNode.m_totalValue = 0f;
        sourceNode.displayColor = Color.green;
        targetNode.displayColor = Color.red;


        //For controlling the while loop
        int attempts = 0;
        bool pathFound = false;
        //The current working set of nodes
        PathNode[] currentSet = new PathNode[] { targetNode };
        //The nodes we are going to search
        List<PathNode> nextSet = new List<PathNode>();

        while (!pathFound && attempts < 10000)
        {
            foreach (PathNode node in currentSet)
            {
                node.m_visited = true;

                //loop through all nodes connected to this one
                foreach (PathNode connectednode in node.connectedNodes)
                {
                    //if it's the source node we're done
                    if(connectednode == sourceNode)
                    {
                        pathFound = true;
                        break;
                    }

                    //Calculate the total value the connected node would have from our node
                    float newConnectionValue = node.m_totalValue + Vector3.Distance(node.transform.position, connectednode.transform.position);

                    //If it's better then the current value in the connected node, store this as it's new value
                    if (newConnectionValue < connectednode.m_totalValue) connectednode.m_totalValue = newConnectionValue;

                    //Add that node to the set of new nodes
                    if (!nextSet.Contains(connectednode)) nextSet.Add(connectednode);
                }

                if (pathFound) break;
            }

            currentSet = nextSet.ToArray();
            nextSet.Clear();

            attempts++;
        }

        List<PathNode> finalPath = new List<PathNode>();
        bool finalPathFound = false;
        int escapeCounter = 0;
        PathNode currentNode = sourceNode;
        PathNode nextNode = null;

        while (!finalPathFound && escapeCounter < 1000)
        {
            float lowestValue = 9999f;
            for (int i = 0; i < currentNode.connectedNodes.Length; i++)
            {
                if (currentNode.connectedNodes[i].m_totalValue < lowestValue)
                {
                    nextNode = currentNode.connectedNodes[i];
                    lowestValue = nextNode.m_totalValue;
                }
            }

            currentNode = nextNode;
            nextNode.displayColor = Color.blue;
            finalPath.Add(nextNode);

            escapeCounter++;
        }

        finalPath[0].displayColor = Color.cyan;

        Debug.Log("Returning " + finalPath[0].transform.position);
        return finalPath[0].transform.position;
    }

    public PathNode GetClosestNode(Vector3 position)
    {
        if (m_allNodes.Length == 0) Debug.LogWarning("Could not find closest node as there are no nodes stored. Run node connector first");

        //Set initial node
        PathNode closest = m_allNodes[0];
        float lastDistance = Vector3.Distance(position, closest.transform.position);
        float distance;
        
        //Loop through all nodes
        for (int i = 1; i < m_allNodes.Length; i++)
        {
            //Get distance between this node and the target
            distance = Vector3.Distance(position, m_allNodes[i].transform.position);
            //if less then the current closest, make this the closest
            if (distance < lastDistance)
            {
                lastDistance = distance;
                closest = m_allNodes[i];
            }
        }

        return closest;
    }
}
