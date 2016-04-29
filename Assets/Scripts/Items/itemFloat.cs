using UnityEngine;
using System.Collections;

public class itemFloat : MonoBehaviour {
    float maxRange = 2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y > maxRange)
        {
            transform.Translate(new Vector3(0, -Time.deltaTime, 0));
        }
        else if(transform.position.y < -maxRange)
            transform.Translate(new Vector3(0, Time.deltaTime, 0));
    }
}
