using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

    // This is bad and we should not be keeping it
    // I feel bad
    private GameObject player;
    public GameObject playerPrefab;
    public GameObject playerCamera;
    public GameObject playerInventory;

    // Use this for initialization
    void Awake ()
	{
	    if (GameObject.FindGameObjectWithTag("Player") == null)
	    {
	        player = Instantiate(playerPrefab);
        }
	    else
	    {
	        player = GameObject.FindGameObjectWithTag("Player");
	    }
        if (GameObject.FindGameObjectWithTag("MainCamera") == null)
        {
            Instantiate(playerCamera);
        }
        if (GameObject.FindGameObjectWithTag("HUD") == null)
        {
            Instantiate(playerInventory);
        }

        playerCamera.gameObject.GetComponent<CameraShift>().player = player;
        player.gameObject.GetComponentInChildren<Weapon>(true).gameObject.SetActive(true);
    }

    void Start()
    {
        player.transform.position = gameObject.transform.position;
    }
}
