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
			player.GetComponent<PlayerCombat> ().Health = 5;
        }
	    else
	    {
	        player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerCombat> ().Health = 5;
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
		if (GameObject.FindGameObjectWithTag("Player") == null)
		{
			player = Instantiate(playerPrefab);
			player.GetComponent<PlayerCombat> ().Health = 5;
			//player.GetComponent<HealthUI> ().SpawnHeart (5);
		}
		else
		{
			player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerCombat> ().Health = 5;
			player.GetComponent<Animator> ().SetTrigger ("Attack");
			//player.GetComponent<HealthUI> ().SpawnHeart (5);
		}

        player.transform.position = gameObject.transform.position;
        player.gameObject.GetComponent<HealthUI>().enabled = true;

		//GameObject.Find("Camera").GetComponent<Camera> ().orthographic = true;
		//playerCamera.GetComponent<Camera> ().orthographic = true;
		//if (InputManager.Instance.is2D) {
		//	playerCamera.GetComponent<Camera> ().orthographic = true;
		//} else {
		//	playerCamera.GetComponent<Camera> ().orthographic = false;
		//}
    }
}
