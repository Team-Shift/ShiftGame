using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    int health;
    public int startHealth = 2;
    public GameObject hitPart;

    void Start()
    {
        health = startHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            health--;
        }
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            Debug.Log("enemy " + this.name + " has died.");
            Instantiate(hitPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
