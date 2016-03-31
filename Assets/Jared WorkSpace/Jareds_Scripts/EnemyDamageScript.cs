using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour
{
    Custom2DController playerScript;
    public GameObject player;
    public float xOffset;
    public float zOffset;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Custom2DController>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (playerScript.CameraSwitch == false)
        {
            if ((player.transform.position.x <= transform.position.x + xOffset && player.transform.position.x >= transform.position.x - xOffset) && (player.transform.position.z <= transform.position.z + zOffset && player.transform.position.z >= transform.position.z - zOffset))
            {
                Debug.Log("Collision via x/z axis!");
                Destroy(gameObject);
                playerScript.DamageFallback(transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Gameobject dead");
            Destroy(gameObject);
            playerScript.DamageFallback(transform.position);
            //Add reference to monster script later to decress health
        }
    }
}
