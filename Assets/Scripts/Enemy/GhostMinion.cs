using UnityEngine;
using System.Collections;

public class GhostMinion : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerCombat>().DamageFallback(gameObject.transform.position);
        }
    }
}
