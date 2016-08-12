using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {


    Collider c;

    void Start()
    {

        //c =  gameObject.GetComponentInChildren<Collider>();

        foreach(Transform temp in gameObject.GetComponentInChildren<Transform>())
        {
            if(temp.name == "Hitbox")
            {
                c = temp.gameObject.GetComponent<Collider>();
            }
        }

       //GameObject g =  GameObject.FindGameObjectWithTag("Hitbox");
        c.gameObject.tag = "Weapon";

        
       //c = g.GetComponent<Collider>();
       //Debug.Log( c.name);
       c.enabled = false;
    }


    void enableCollider()
    {
        if(c)
            c.enabled = true;
    }

    void disableCollider()
    {
        if(c)
            c.enabled = false;
    }
}
