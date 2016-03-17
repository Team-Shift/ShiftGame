using UnityEngine;
using System.Collections;
using Shift;
using UnityEditor;

[CustomEditor(typeof(DungeonGen))]
public class DungeonEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DungeonGen dungeon = target as DungeonGen;

        if (DrawDefaultInspector())
        {
            dungeon.GenerateDungeon();
        }

        if (GUILayout.Button("Generate Map"))
        {
            dungeon.GenerateDungeon();
        }
    }
}