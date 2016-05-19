using UnityEngine;
using System.Collections;

public class GhostMinion : MonoBehaviour
{
    public float lifeTime = 0;

    // Use this for initialization
    void Update()
    {
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerCombat>().DamageFallback(gameObject.transform.position);
            Destroy(gameObject);
        }
        if (col.tag == "Hitbox")
        {
            Destroy(gameObject);
        }
    }
}
