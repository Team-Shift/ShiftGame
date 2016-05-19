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

    //[HideInInspector]
    public Vector3 targetPosition;
    [HideInInspector]
    public Room.Direction targetDirection;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.position = targetPosition;
        }

        //ToDo Trigger OnEntrance of a Room
    }
}
