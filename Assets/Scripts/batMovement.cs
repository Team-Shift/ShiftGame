using UnityEngine;
using System.Collections;

public class batMovement : MonoBehaviour {

    private Wander w;
    private Seek s;

    // Use this for initialization
    void Start () {
        w = GetComponent<Wander>();
        s = GetComponent<Seek>();
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            s.shouldSeek = true;
            w.shouldWander = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            s.shouldSeek = false;
            w.shouldWander = true;
        }
    }
}
