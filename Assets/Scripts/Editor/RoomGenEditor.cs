using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class RoomGenEditor:EditorWindow {

	string myString = "Hello World";
	bool shouldEdit;
	bool myBool = true;
	float myFloat = 1.23f;

	int x,y;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Create/Make Room")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(RoomGenEditor));
		CreateRoom cr;
	}

	void OnGUI()
	{
		
		GUILayout.Label ("Tiles Settings", EditorStyles.boldLabel);
		x = EditorGUILayout.IntField ("Num Y Tiles", x);
		y = EditorGUILayout.IntField ("Num Y Tiles", y);

		shouldEdit = EditorGUILayout.BeginToggleGroup ("Edit Room", shouldEdit);

		//myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		//myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();

		if (GUILayout.Button ("Create")) {
			Debug.Log ("it worked");
		}
	}
}
	