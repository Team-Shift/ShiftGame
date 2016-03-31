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

    public Vector3 targetPosition;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player touched the butt");
            Debug.Log("Player going to: " + targetPosition.x + ", " + targetPosition.z);
            col.transform.position = targetPosition + new Vector3(0,4,0);


            //col.transform.position += new Vector3(0, col.transform.position.y + col.bounds.max.y*2, 0);


        }
    }

    private void Teleport(Collider col, Transform position)
    {
        
    }
}
