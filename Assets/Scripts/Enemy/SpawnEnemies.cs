using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	public GameObject ghost;
	public GameObject spider;
	public GameObject bat;

	GameObject player;
	// Use this for initialization

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		ghost.GetComponent<Wander> ().enabled = false;
		ghost.GetComponent<SphereCollider> ().radius = 8;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			SpawnGhosts ();
		}
		if (Input.GetKeyDown (KeyCode.B)) 
		{
			SpawnBats ();
		}
		if (Input.GetKeyDown (KeyCode.M)) 
		{
			SpawnTurrets ();
		}
	}

	void SpawnGhosts()
	{
		Instantiate (ghost, new Vector3(player.transform.position.x + 3, player.transform.position.y, player.transform.position.z), Quaternion.identity);
		Instantiate (ghost, new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z), Quaternion.identity);
		Instantiate (ghost, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3), Quaternion.identity);
	}

	void SpawnBats()
	{
		Instantiate (bat, new Vector3(transform.position.x + 2.5f, player.transform.position.y, transform.position.z), Quaternion.Euler(0,180,0));
		Instantiate (bat, new Vector3(transform.position.x - 2.5f, player.transform.position.y, transform.position.z), Quaternion.Euler(0,180,0));
	}

	void SpawnTurrets ()
	{
		Instantiate (spider, new Vector3(transform.position.x + 3, player.transform.position.y, transform.position.z), Quaternion.Euler(0,180,0));
		Instantiate (spider, new Vector3(transform.position.x - 3, player.transform.position.y, transform.position.z), Quaternion.Euler(0,180,0));
	}
}
