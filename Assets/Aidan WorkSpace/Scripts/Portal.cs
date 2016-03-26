using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour {

	// Use this for initialization
	private void Start () {
        Debug.Log("Parent Name" + transform.GetComponentInParent<Room>().name);
        //neighborLocations = new Dictionary<Room.Direction, Transform>(transform.GetComponentInParent<Room>().neighbors);
        Debug.Log(transform.GetComponentInParent<Room>().neighbors.Count);
        //neighborLocations = transform.GetComponentInParent<Room>().neighbors;
    }
	
	// Update is called once per frame
    private void Update()
    {

    }

    private Dictionary<Room.Direction, Transform> neighborLocations; 

    //public Dictionary<Room.Direction, Transform> neighborLocations;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player touched the butt");

            Vector3 foo = neighborLocations[Room.Direction.North].position;

            Debug.Log(foo.ToString());
            col.transform.position = foo + new Vector3(0,4,0);


            //col.transform.position += new Vector3(0, col.transform.position.y + col.bounds.max.y*2, 0);


        }
    }

    private void Teleport(Collider col, Transform position)
    {
        
    }
}
