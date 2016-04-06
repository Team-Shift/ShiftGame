﻿using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    bool inRange;
    GameObject objToFollow;

    public float yPosLock;
    public GameObject projectile;
    public bool alwaysShoot;
    public bool shouldRotate;
    public float yOffset;
    public float rotOffset;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if(anim == null)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }

        shouldRotate = false;
        alwaysShoot = false;
    }

    void Update()
    {
        
        if (inRange)
        {
            // play turret anim
            anim.SetBool("canShoot", true);
            if (shouldRotate)
            {
                // rotate turret to follow
                Transform t = objToFollow.transform;
                t.position = new Vector3(t.position.x , yPosLock, t.position.z);
                gameObject.transform.LookAt(t);
            }
        }
        else if(alwaysShoot) anim.SetBool("canShoot", true);
        else anim.SetBool("canShoot", false);
        
    }

    void OnTriggerEnter(Collider other)
    {
        // only fire if collider is player
        if (other.tag == "Player" )
        {
            inRange = true;
            alwaysShoot = true;
            shouldRotate = true;
            Debug.Log("inRange");
            objToFollow = GameObject.FindGameObjectWithTag("Player");
        }
        // lock y
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" )
        {
            inRange = true;
            alwaysShoot = true;
            shouldRotate = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            alwaysShoot = false;
            shouldRotate = false;
        }
    }

    void ShootProjectile()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yOffset, gameObject.transform.position.z);

        Quaternion rot = gameObject.transform.rotation;
        rot *= Quaternion.Euler(0, rotOffset, 0); // rotating wierdly 
        // instantiate fire
        Instantiate(projectile, pos, rot);
    }
}
