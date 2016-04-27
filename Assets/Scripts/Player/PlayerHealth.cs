using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth = 3;
    public int health;

    void Start()
    {
        health = maxHealth;
    }

	//void OnCollisionEnter(Collider2D other)
 //   {
 //       if(other.tag == "enemy")
 //       {
 //           health--;
 //       }
 //   }

    void OnCollisionEnter(/*Collider other*/)
    {
        //if (other.tag == "enemy")
        //{
            health--;
        //}
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
