using UnityEngine;
using System.Collections;

public class WeaponAttack : MonoBehaviour {

    Collider c;
	// Use this for initialization
	void Start () {
        c = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider>();
        c.enabled = false;
	}

    public void enableCollider()
    {
        c.enabled = true;
    }

    public void disableCollider()
    {
        c.enabled = false;
    }
}
