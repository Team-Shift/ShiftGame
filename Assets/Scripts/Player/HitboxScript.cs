using UnityEngine;
using System.Collections;

public class HitboxScript: MonoBehaviour {
    Collider c;

    void Start()
    {
        Transform t = gameObject.transform.FindChild("Hitbox");
        GameObject g = t.gameObject;
        c =  g.GetComponent<Collider>();
        Debug.Log(c.name);
        g.tag = "Weapon";

        
       c = g.GetComponent<Collider>();
       //Debug.Log( c.name);
       c.enabled = false;
    }


    void enableCollider()
    {
        if (c)
        {
            c.enabled = true;
        }
    }

    void disableCollider()
    {
        if (c)
        {
            c.enabled = false;
        }
    }
}
