﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour {

    Custom2DController playerScript;
    GameObject player;
    public int health;
    public int startHealth = 5;
    public GameObject hitPart;
    public GameObject parent;
    private AudioSource enemySound;
    public AudioClip hurtSound;

    public float xOffset;
    public float zOffset;
    private float yOffset;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Custom2DController>();

        enemySound = gameObject.GetComponent<AudioSource>();
        parent = this.gameObject.transform.parent.gameObject;
        Debug.Log(this.gameObject.transform.parent.gameObject.name);
        health = startHealth;
    }

    // triggers when hitting sphere collider.... (might need to separate colliders)
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Debug.Log("losing health from " + other.name);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        enemySound.PlayOneShot(hurtSound);
        Instantiate(hitPart, transform.position, Quaternion.identity);
        health--;
    }

    void FixedUpdate()
    {
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 playerPos = player.transform.Find("Hitbox").gameObject.transform.position;

        yOffset = GetYOffset();

        //2d
        if (playerScript.CameraSwitch == false)
        {
            //if ((player.transform.position.x <= transform.position.x + xOffset && player.transform.position.x >= transform.position.x - xOffset) && (player.transform.position.z <= transform.position.z + zOffset && player.transform.position.z >= transform.position.z - zOffset))
            if (Mathf.Abs(playerPos.x - enemyPos.x) < xOffset && Mathf.Abs(playerPos.z - (enemyPos.z + yOffset)) < zOffset)
            {
                if (playerScript.melee == true && Input.GetKeyDown(KeyCode.Mouse1))
                {
                    TakeDamage();
                }
            }
        }

        if (health <= 0)
        {
            Debug.Log("enemy " + this.name + " has died.");
            Instantiate(hitPart, transform.position, Quaternion.identity);
            Destroy(parent);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 gizmosPos = new Vector3(parent.transform.position.x, 0, parent.transform.position.z + yOffset);
        Vector3 gizmosSize = new Vector3(xOffset, 1, zOffset);
        Gizmos.DrawWireCube(gizmosPos, gizmosSize);
    }

    float GetYOffset()
    {
        float a = 0;

        float angleB = 180 - (Camera.main.transform.eulerAngles.x + 90);
        angleB = Mathf.Round(angleB);
        Vector3 tmp = new Vector3(angleB, 0, 0);

        a = Mathf.Tan((tmp.x * Mathf.PI) / 180) * parent.transform.position.y;

        return a;
    }
}
