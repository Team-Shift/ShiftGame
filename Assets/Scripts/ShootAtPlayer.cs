using UnityEngine;
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

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
                t.position = new Vector3(t.position.x, yPosLock, t.position.z);
                gameObject.transform.LookAt(t);
            }
        }
        else if(alwaysShoot) anim.SetBool("canShoot", true);
        else anim.SetBool("canShoot", false);
    }

    void OnTriggerEnter(Collider other)
    {
        // only fire if collider is player
        if (other.tag == "Player")
        {
            inRange = true;
            Debug.Log("inRange");
            objToFollow = GameObject.FindGameObjectWithTag("Player");
        }
        // lock y
    }

    void OnTriggerExit()
    {
        inRange = false;
    }

    void ShootProjectile()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

        Quaternion rot = gameObject.transform.rotation;
        rot *= Quaternion.Euler(0, 90, 0); // rotating wierdly 
        // instantiate fire
        Instantiate(projectile, pos, rot);
    }
}
