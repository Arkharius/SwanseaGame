using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathNode : MonoBehaviour {

    [Range(0f, 5f)]
    public float m_pathRadius = .5f;

    public PathNode[] connectedNodes;

    public bool m_visited = false;
    public float m_totalValue = 0f;
    public Color displayColor = Color.yellow;

    private void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }

    private void OnDrawGizmos()
    {
        if (Pathfinder.showAll)
            DrawGizmos();
    }

    private void DrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (PathNode node in connectedNodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }

        Gizmos.color = displayColor;

        Gizmos.DrawWireSphere(transform.position, m_pathRadius);

        

        Gizmos.color = Color.white;
    }
}
