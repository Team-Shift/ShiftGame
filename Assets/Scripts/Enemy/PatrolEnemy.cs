using UnityEngine;
using System.Collections;

public class PatrolEnemy : MonoBehaviour {
    Seek s;
    Wander w;
    Transform playerPos;
    Collider c;
    Animator anim;

	public float atkRange = 0.7f;
    // Use this for initialization
    void Start()
    {
        s = GetComponent<Seek>();
        w = GetComponent<Wander>();
        c = gameObject.GetComponentInChildren<Collider>();
        anim = GetComponentInChildren<Animator>();
        //playerPos
    }

    // Update is called once per frame
    void Update()
    {
        if(s.shouldSeek)
        {
            Vector3 dir = playerPos.position - gameObject.transform.position;
			if (dir.magnitude <= atkRange)
            {
                anim.SetBool("canAttack", true);
				if(dir.magnitude <= .2f)
                s.shouldSeek = false;
            }
            else
            {
                anim.SetBool("canAttack", false);
                s.shouldSeek = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            s.shouldSeek = true;
            s.SetObjToSeek(other.transform.position);
            w.shouldWander = false;
            playerPos = other.transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            s.shouldSeek = true;
            s.SetObjToSeek(other.transform.position);
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
