using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour {

	// Use this for initialization
	private void Start () {

    }
	
	// Update is called once per frame
    private void Update()
    {

    }

    [HideInInspector]
    public Vector3 targetPosition;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
                Debug.Log("Player going to: " + targetPosition.x + ", " + targetPosition.z);
                col.transform.position = targetPosition + new Vector3(0, 5, 0);
        }
    }
}
