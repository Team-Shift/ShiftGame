using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Wander))]
public class CustomWanderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Wander myscript = (Wander)target;

        if(GUILayout.Button("Add Path Node"))
        {
            myscript.AddNode();
        }
    }
}
