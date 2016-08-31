using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    //[HideInInspector]
    public bool alwaysShoot;
    //[HideInInspector]
    public bool shouldRotate;

    private bool inRange;
    GameObject objToFollow;
    public float yPosLock;
    public GameObject projectile;
    public float yOffset;
    public float rotOffset;
	public float zOffset;
    public bool isFlyingEnemy;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if(anim == null)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }
        if (!isFlyingEnemy)
        {
            //shouldRotate = false;
        }
        alwaysShoot = false;
		objToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
		if (inRange) {
			if (anim) {
				// play turret anim
				anim.SetBool ("canShoot", true);
			}
			if (shouldRotate) {
				// rotate turret to follow
				Transform t = objToFollow.transform;
				//Debug.Log (t.position);
				//t.position = new Vector3(-t.position.x , t.position.y, -t.position.z);
				//t.position = new Vector3(t.position.x, t.position.y, -t.position.z);
				if (!isFlyingEnemy) {
					gameObject.transform.LookAt (new Vector3 (t.position.x, this.transform.position.y, t.position.z));
				} else {
					gameObject.transform.LookAt (t);
				}
			}
		} else if (alwaysShoot && !anim)
			anim.SetBool ("canShoot", true);
		else if(!anim){
			anim.SetBool ("canShoot", false);
		}
        
    }

    void OnTriggerEnter(Collider other)
    {
        // only fire if collider is player
        if (other.tag == "Player" )
        {
            inRange = true;
            if (isFlyingEnemy)
            {
                alwaysShoot = true;
                shouldRotate = true;
            }
            //Debug.Log("inRange");
            
        }
        // lock y
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" )
        {
            inRange = true;
            if (isFlyingEnemy)
            {
                alwaysShoot = true;
                shouldRotate = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            if (isFlyingEnemy)
            {
                alwaysShoot = false;
                shouldRotate = false;
            }
        }
    }

    void ShootProjectile()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yOffset, gameObject.transform.position.z);


        Quaternion rot = gameObject.transform.rotation;
		rot *= Quaternion.Euler(0, rotOffset, zOffset); // rotating wierdly 

        // instantiate fire
        Instantiate(projectile, pos, rot);
    }
}
