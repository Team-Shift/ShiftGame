using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float speed;

	void Start () {
	
	}

	void Update () {
	
	}

    // should be different 3D 2D
    void Wander()
    {
        // 2D back and forth

        // 3D wander
    }

    void Seek()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

      ///  gameObject.transform.position = ;   
    }

    void OnTriggerEnter()
    {
        Seek();
    }
}
