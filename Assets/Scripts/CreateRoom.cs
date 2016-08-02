using UnityEngine;
using System.Collections;

public class CreateRoom : MonoBehaviour {

	public void createTiles(string name, int xTiles, int yTiles, bool[,]toggles)
	{
		GameObject newRoom = GameObject.Find(name);
		if(newRoom)
		{
			DestroyImmediate (newRoom);
			Debug.Log ("destorying obj");
		}
		newRoom = new GameObject ();
		newRoom.name = name;
		for (int i = 0; i < xTiles; i++) 
		{
			for (int j = 0; j < yTiles; j++) 
			{
				if (!toggles [i, j]) {
					GameObject temp = Object.Instantiate (Resources.Load ("forestfloor_center"), new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
					temp.transform.SetParent (newRoom.transform);
				}
			}
		}
		GameObject north = Object.Instantiate (Resources.Load ("North_Hallway"), new Vector3 (Mathf.CeilToInt (xTiles / 2), 0, -2), Quaternion.identity) as GameObject;
		GameObject south = Object.Instantiate (Resources.Load ("South_Hallway"), new Vector3 (Mathf.CeilToInt (xTiles / 2), 0, yTiles+2), Quaternion.identity) as GameObject;
		GameObject west = Object.Instantiate (Resources.Load ("West_Hallway"), new Vector3 (-2, 0, Mathf.CeilToInt (yTiles / 2)), Quaternion.identity) as GameObject;
		GameObject east = Object.Instantiate (Resources.Load ("East_Hallway"), new Vector3 (yTiles+2, 0, Mathf.CeilToInt (yTiles / 2)), Quaternion.identity) as GameObject;
		north.transform.SetParent (newRoom.transform);
		south.transform.localRotation= Quaternion.Euler (0, 180, 0);
		south.transform.SetParent (newRoom.transform);
		west.transform.localRotation= Quaternion.Euler (0, 90, 0);
		west.transform.SetParent (newRoom.transform);
		east.transform.SetParent (newRoom.transform);
		east.transform.localRotation= Quaternion.Euler (0, -90, 0);
	}
}
