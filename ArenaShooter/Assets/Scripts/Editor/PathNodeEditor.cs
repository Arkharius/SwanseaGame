using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{

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

        Pathfinder.blockingLayer = EditorGUILayout.TextField(Pathfinder.blockingLayer);
        Pathfinder.defaultSearchRange = EditorGUILayout.FloatField(Pathfinder.defaultSearchRange);

        if (GUILayout.Button("Run connector"))
            Pathfinder.ConnectNodes(Pathfinder.blockingLayer, Pathfinder.defaultSearchRange);
    }
}
