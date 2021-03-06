﻿using UnityEngine;
using System.Collections;

// projectile damaging player
public class EnemyDamageScript : MonoBehaviour
{
    PlayerCombat playerScript;
    public GameObject player;
    public float xOffset;
    public float zOffset;
    [HideInInspector]
    private float yOffset;
    public bool isProjectile;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerCombat>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 fireBallPos = gameObject.transform.position;

        yOffset = fireBallPos.y * .84f;

        if (InputManager.Instance.is2D)
        {
            //if ((player.transform.position.x <= transform.position.x + xOffset && player.transform.position.x >= transform.position.x - xOffset) && (player.transform.position.z <= transform.position.z + zOffset && player.transform.position.z >= transform.position.z - zOffset))
            //if( (playerPos.x <= fireBallPos.x + xOffset && playerPos.x >= fireBallPos.x - xOffset) && (playerPos.z <= (fireBallPos.z + yOffset) + zOffset && playerPos.z >= (fireBallPos.z + yOffset) - zOffset) )
            if (Mathf.Abs(player.transform.position.x - fireBallPos.x) <= xOffset && Mathf.Abs(player.transform.position.z - fireBallPos.z) <= zOffset)
            {
                Destroy(gameObject);
                playerScript.DamageFallback(transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hitting");
		if (other.tag == "Player") {
			if (isProjectile) {
				Destroy (gameObject);
			}
			playerScript.DamageFallback (transform.position);
		}

    }
}
