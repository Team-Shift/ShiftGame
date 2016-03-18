using UnityEngine;
using System.Collections;

public class MeleeWeapon_Script : MonoBehaviour
{
    public int damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy was hit");
            //Add reference to monster script later to decress health
        }
    }
}
