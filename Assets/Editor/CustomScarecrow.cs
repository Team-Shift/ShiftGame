using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ScareCrow))]
public class CustomScarecrow : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScareCrow myScript = (ScareCrow)target;

        if (GUILayout.Button("Add Path Node"))
        {
            Selection.activeGameObject = myScript.AddNode();
        }
    }

}
