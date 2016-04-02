using UnityEngine;
using System.Collections;

public class batMovement : MonoBehaviour {

    private Wander w;
    private ShootAtPlayer s;

    // Use this for initialization
    void Start () {
        w = GetComponent<Wander>();
        s = GetComponent<ShootAtPlayer>();
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            w.shouldWander = false;
            s.alwaysShoot = true;
            s.shouldRotate = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            w.shouldWander = true;
            s.alwaysShoot = false;
            s.shouldRotate = false;
        }
    }
}
