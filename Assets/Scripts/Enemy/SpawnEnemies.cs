using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour {
	public GameObject ghost;
	public GameObject spider;
	public GameObject bat;

	public float spawnInterval;
	private float spawnTimer;
	private float playerYPos;

	public List<GameObject> spawnedEnemies;

	GameObject player;
	// Use this for initialization

	void Start () {
		spawnTimer = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerYPos = player.transform.position.y + .1f;

		spawnedEnemies = new List<GameObject> ();

		ghost.GetComponent<Wander> ().enabled = false;
		ghost.GetComponent<SphereCollider> ().radius = 8;
	}
	
	// Update is called once per frame
	void Update () {

		spawnTimer += Time.deltaTime;
		// change to spawn on commands
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
		// spawn next set of enemies
		if (spawnTimer > spawnInterval) 
		{
			spawnTimer = 0;
			// reset timer
			// only spawn if all enemies are dead
			if (AllEnemiesDead()) {
				if (player.transform.position.y >= playerYPos) {
					SpawnBats ();
				} else {
					SpawnGhosts ();
				}
				//SpawnGhosts ();
			}

		}
	}

	void SpawnGhosts()
	{
		spawnedEnemies.Add(Instantiate (ghost, new Vector3(player.transform.position.x + 3, playerYPos, player.transform.position.z), Quaternion.identity)as GameObject);
		spawnedEnemies.Add(Instantiate (ghost, new Vector3(player.transform.position.x - 3, playerYPos, player.transform.position.z), Quaternion.identity)as GameObject);
		spawnedEnemies.Add(Instantiate (ghost, new Vector3(player.transform.position.x, playerYPos, player.transform.position.z + 3), Quaternion.identity)as GameObject);
	}

	void SpawnBats()
	{
		spawnedEnemies.Add(Instantiate (bat, new Vector3(transform.position.x + 2.5f, playerYPos, transform.position.z -2), Quaternion.Euler(0,180,0))as GameObject);
		spawnedEnemies.Add(Instantiate (bat, new Vector3(transform.position.x - 2.5f, playerYPos, transform.position.z -2), Quaternion.Euler(0,180,0))as GameObject);
	}

	public void SpawnTurrets ()
	{
		Instantiate (spider, new Vector3(transform.position.x + 4, playerYPos, transform.position.z), Quaternion.Euler(0,180,0));
		Instantiate (spider, new Vector3(transform.position.x - 4, playerYPos, transform.position.z), Quaternion.Euler(0,180,0));
	}

	bool AllEnemiesDead()
	{
		for(int i =0; i< spawnedEnemies.Count; i++)
		{
			if (spawnedEnemies[i] == null) 
			{
				spawnedEnemies.Remove (spawnedEnemies [i]);
			}
		}
		if (spawnedEnemies.Count < 1) {
			return true;
		} else 
		{
			
			return false;
		}

	}
}
