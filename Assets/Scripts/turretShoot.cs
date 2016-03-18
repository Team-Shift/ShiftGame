using UnityEngine;
using System.Collections;

public class turretShoot : MonoBehaviour {

    private Animator anim;          // get turret animator
    public GameObject fireball;     // prefab
    private GameObject objToFollow;

    public bool inRange;           // when to fire

	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (inRange)
        {
            // play turret anim
            anim.SetBool("canShoot", true);

            // rotate turret to follow
            gameObject.transform.LookAt(objToFollow.transform);
        }
        else anim.SetBool("canShoot", false);
	}

    void OnTriggerEnter(Collider other)
    {
        // only fire if collider is player
        if (other.tag == "Player")
        {
            Debug.Log(other.name);
            inRange = true;
            objToFollow = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void OnTriggerExit()
    {
        inRange = false;
    }

    void ShootProjectile()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x , gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);

        Quaternion rot = gameObject.transform.rotation;
        rot *= Quaternion.Euler(0, 90, 0); // rotating wierdly 
        // instantiate fire
        Instantiate(fireball, pos, rot);
        Debug.Log("uivgbhreiuhiueh");
    }
}
