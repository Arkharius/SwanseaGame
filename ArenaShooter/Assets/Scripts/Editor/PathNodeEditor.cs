using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{

    LayerMask m_pathBlockers;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        if (!Pathfinder.showAll)
        {
            if (GUILayout.Button("Show all nodes"))
                Pathfinder.showAll = true;
        }
        else
        {
            if (GUILayout.Button("Hide other nodes"))
                Pathfinder.showAll = false;
        }

        m_pathBlockers = EditorGUILayout.LayerField(m_pathBlockers);


        if (GUILayout.Button("Run connector"))
            Pathfinder.ConnectNodes(m_pathBlockers);
    }
}
