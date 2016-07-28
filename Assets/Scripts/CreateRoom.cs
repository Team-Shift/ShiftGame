using UnityEngine;
using System.Collections;

public class CreateRoom : MonoBehaviour {

	public void createTiles(string name, int xTiles, int yTiles)
	{
		GameObject newRoom = new GameObject ();
		newRoom.name = name;
		for (int i = 0; i < xTiles; i++) 
		{
			for (int j = 0; j < yTiles; j++) 
			{
				GameObject temp = Object.Instantiate (Resources.Load ("forestfloor_center"), new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
				temp.transform.SetParent (newRoom.transform);
			}
		}
	}
}
