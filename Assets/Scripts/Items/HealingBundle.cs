using UnityEngine;
using System.Collections;

public class HealingBundle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Custom2DController playerController = col.gameObject.GetComponent<Custom2DController>();
            Debug.Log(col.name);
            if (playerController == null)
            {
                Debug.LogError("No Player Controller Found");
            }

            else
            {
                ItemManager.DestroyItem(gameObject);
                Debug.Log("Being picked up by" + playerController.gameObject.name);
            }
        }
    }
}
