using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
    Collider c;

    void Start()
    {
       GameObject g =  GameObject.FindGameObjectWithTag("Hitbox");
        g.tag = "Weapon";

        
       c = g.GetComponent<Collider>();
       //Debug.Log( c.name);
       c.enabled = false;
    }


    void enableCollider()
    {
        c.enabled = true;
    }

    void disableCollider()
    {
       c.enabled = false;
    }
}
