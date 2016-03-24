using UnityEngine;
using System.Collections;

public class PlayerProjectialControl : MonoBehaviour
{
    public float lifeSpan = 0;


    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeSpan);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy was hit");
            //Add reference to monster script later to decress health

            Destoy(gameobject);
        }
    }
}
