using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    int health;
    public int startHealth = 5;
    public GameObject hitPart;
    Rigidbody rb;

    void Start()
    {
        health = startHealth;
        rb = GetComponent<Rigidbody>();
    }

    // triggers when hitting sphere collider.... (might need to separate colliders)
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            Debug.Log("losing health from " + other.name);
            TakeDamage();
            // knockback
        }
    }

    void TakeDamage()
    {
        health--;
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
