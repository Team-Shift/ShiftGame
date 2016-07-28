using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class RoomGenEditor:EditorWindow {

	string myString = "Hello World";
	bool shouldEdit;

	int x,y;
	string roomName;
	public static CreateRoom instance;

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
		roomName = EditorGUILayout.TextField ("Name of Room", roomName);
		x = EditorGUILayout.IntField ("Num Y Tiles", x);
		y = EditorGUILayout.IntField ("Num Y Tiles", y);

		shouldEdit = EditorGUILayout.BeginToggleGroup ("Edit Room", shouldEdit);
		// draw layout of room 
		EditorGUILayout.EndToggleGroup ();

		if (GUILayout.Button ("Create")) {
			instance = new CreateRoom();
			instance.createTiles (roomName,x, y);
		}
	}
}
	