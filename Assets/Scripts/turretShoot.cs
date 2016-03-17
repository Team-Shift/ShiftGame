using UnityEngine;
using System.Collections;

public class turretShoot : MonoBehaviour {

    private Animator anim;

    public bool inRange;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (inRange)
        {
            anim.SetBool("canShoot", true);
        }
        else
            anim.SetBool("canShoot", false);
	}

    void OnTriggerEnter()
    {
        inRange = true;
    }

    void OnTriggerExit()
    {
        inRange = false;
    }
}
