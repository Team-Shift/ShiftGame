using UnityEngine;
using System.Collections;

public class CreateRoom : MonoBehaviour {

	public void createTiles(int xTiles, int yTiles)
	{
		for (int i = 0; i < xTiles; i++) 
		{
			for (int j = 0; j < yTiles; j++) 
			{
				Instantiate (Resources.Load("forestfloor_center"), new Vector3(xTiles,0,yTiles));
			}
		}
	}
}
