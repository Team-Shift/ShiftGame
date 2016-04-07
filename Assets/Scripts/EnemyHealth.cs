using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    //Custom2DController playerScript;
    //GameObject player;
    int health;
    public int startHealth = 5;
    public GameObject hitPart;
    private GameObject parent;

    //public float xOffset;
    //public float zOffset;
    //private float yOffset;


    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        health = startHealth;
        //player = GameObject.Find("Player");
        //playerScript = player.GetComponent<Custom2DController>();
    }

    // triggers when hitting sphere collider.... (might need to separate colliders)
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Debug.Log("losing health from " + other.name);
            TakeDamage();
            // knockback
        }
    }

    public void TakeDamage()
    {
        Instantiate(hitPart, transform.position, Quaternion.identity);
        health--;
    }

    void FixedUpdate()
    {
        // 2D/3D collision detection
        // gameObject <-- playerPos
        // player <-- fireball
        //Vector3 enemyPos = gameObject.transform.position;
        //Vector3 playerPos = player.transform.position;

        //yOffset = playerPos.y * .84f;

        //2d
        //if (playerScript.CameraSwitch == false)
        //{
        //    //if ((player.transform.position.x <= transform.position.x + xOffset && player.transform.position.x >= transform.position.x - xOffset) && (player.transform.position.z <= transform.position.z + zOffset && player.transform.position.z >= transform.position.z - zOffset))
        //    if ((enemyPos.x <= playerPos.x + xOffset && enemyPos.x >= playerPos.x - xOffset) && (enemyPos.z <= (playerPos.z + yOffset) + zOffset && enemyPos.z >= (playerPos.z + yOffset) - zOffset))
        //    {
        //        Debug.Log(gameObject.name + " is taking damage");
        //        Destroy(gameObject);
        //        TakeDamage();
        //    }
        //}

        if (health <= 0)
        {
            Debug.Log("enemy " + this.name + " has died.");
            Instantiate(hitPart, transform.position, Quaternion.identity);
            Destroy(parent);
        }
    }
}
