using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour
{
    Custom2DController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Custom2DController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player was hit");
            player.DamageFallback(transform.position);
            //Add reference to monster script later to decress health
        }
    }
}
