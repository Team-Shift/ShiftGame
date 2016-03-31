﻿using UnityEngine;
using System.Collections;

public class EnemyDamageScript : MonoBehaviour
{
    Custom2DController playerScript;
    public GameObject player;
    public float xOffset;
    public float zOffset;
    [HideInInspector]
    private float yOffset;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Custom2DController>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 fireBallPos = gameObject.transform.position;

        yOffset = fireBallPos.y * .84f;
        Debug.Log(yOffset);

        if (playerScript.CameraSwitch == false)
        {
            //if ((player.transform.position.x <= transform.position.x + xOffset && player.transform.position.x >= transform.position.x - xOffset) && (player.transform.position.z <= transform.position.z + zOffset && player.transform.position.z >= transform.position.z - zOffset))
            if( (playerPos.x <= fireBallPos.x + xOffset && playerPos.x >= fireBallPos.x - xOffset) && (playerPos.z <= (fireBallPos.z + yOffset) + zOffset && playerPos.z >= (fireBallPos.z + yOffset) - zOffset) )
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
<<<<<<< 4440d3a0fd7f2e0ff3153fe987ad2987c7317555
            Debug.Log("Gameobject dead");
            Destroy(gameObject);
            playerScript.DamageFallback(transform.position);
||||||| merged common ancestors
            Debug.Log("Player was hit");
            player.DamageFallback(transform.position);
=======
            Debug.Log("Player was hit");
            //player.DamageFallback(transform.position);
>>>>>>> combat
            //Add reference to monster script later to decress health
        }
        Destroy(gameObject);
    }
}
