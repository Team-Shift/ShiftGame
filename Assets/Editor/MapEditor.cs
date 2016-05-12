using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(MapGenerator))]
public class MapEditor : Editor {

    public override void OnInspectorGUI()
    {
        MapGenerator map = target as MapGenerator;

        DrawDefaultInspector();

        if(GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }
}