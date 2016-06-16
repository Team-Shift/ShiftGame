using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

    // This is bad and we should not be keeping it
    // I feel bad
    private GameObject player;


	// Use this for initialization
	void Start ()
	{
	    player = GameObject.FindGameObjectWithTag("Player");

	    player.transform.position = gameObject.transform.position;
	}
}
