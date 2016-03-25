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

<<<<<<< cb066555a42ee0e1c842264481ec0110d00cea5b
            Destroy(gameObject);
||||||| merged common ancestors
            Destoy(gameobject);
=======
            //Destoy(gameobject);
>>>>>>> added assets for inventory
        }
    }
}
