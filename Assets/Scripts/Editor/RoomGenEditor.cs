using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class RoomGenEditor:EditorWindow {

	string myString = "Hello World";
	bool shouldEdit = false;

	static int MAXVALUE = 25;
	bool buttonState;
	static bool [,]tiles= new bool[MAXVALUE, MAXVALUE];
	int x ,y;
	string roomName;
	public static CreateRoom instance;

	int selected = 2;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Create/Make Room")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(RoomGenEditor));
		CreateRoom cr;
		InitArray (tiles);
	}

	static void InitArray(bool [,] arr)
	{
		for (int j = 0; j < MAXVALUE; j++) {
			for (int i = 0; i < MAXVALUE; i++) {
				arr [j, i] = false;
			}
		}
	}

	void OnGUI()
	{
		GUILayout.Label ("Tiles Settings", EditorStyles.boldLabel);
		roomName = EditorGUILayout.TextField ("Name of Room", roomName);

		x = EditorGUILayout.IntField ("Num Y Tiles", x);
		if (x > MAXVALUE)
			x = MAXVALUE;
		y = EditorGUILayout.IntField ("Num Y Tiles", y);
		if (y > MAXVALUE)
			y = MAXVALUE;

		GUILayout.Label ("Edit Room", EditorStyles.boldLabel);

		// draw layout of room 
		for (int j = 0; j < y; j++) {
			GUILayout.BeginHorizontal ();
			for (int i = 0; i < x; i++) {
				// do shit here
				tiles[j,i] = GUILayout.Toggle(tiles[j,i], "", "button", GUILayout.Width(20), GUILayout.Height(20));
				//tiles[j,i] = ! tiles[j,i];
			}
			GUILayout.EndHorizontal ();
		}
			
		if (GUILayout.Button ("Create")) {
			instance = new CreateRoom();
			instance.createTiles (roomName,x, y, tiles);
		}
	}
}
	