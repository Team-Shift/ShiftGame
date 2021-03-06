﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour {

    PlayerCombat playerScript;
    GameObject player;
    public int health;
    public int startHealth = 5;
    public GameObject hitPart;
	public GameObject itemToDrop;
	// 1 out of int chance of getting dropping
	public int dropRate;

   // public GameObject parent;
    private AudioSource enemySound;
    public AudioClip hurtSound;

	public bool isBat;
	public bool isBoss;
    public float xOffset;
    public float zOffset;
    private float yOffset;
	GameObject collider;
	public float zCol;

    void Start()
    {
		if (isBat) 
		{
			collider = new GameObject ();
			collider.name = "2D Collider";
		}
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerCombat>();

        enemySound = gameObject.GetComponent<AudioSource>();
        
        //parent = this.gameObject.transform.parent.gameObject;
        health = startHealth;

		//if drop rate is unnassigned
		if (dropRate == null || dropRate==0) {
			//Debug.Log ("not assigned");
			dropRate = 50;
		}
		if (!itemToDrop) {
			itemToDrop = Resources.Load ("Items/Healing Potion")as GameObject;
		}
    }

    // triggers when hitting sphere collider.... (might need to separate colliders)
    void OnTriggerEnter(Collider other)
    {
		//Debug.Log (other.tag);
		if (other.tag == "Hitbox" && other.tag != "Enemy") {
			TakeDamage ();
			//Debug.Log ("damage by " + other.name);
		}
    }

	void Update()
	{
		if (isBat) 
		{
			GetCollider ();
		}
	}

    public void TakeDamage()
    {
        //enemySound.PlayOneShot(hurtSound);
        Instantiate(hitPart, transform.position, Quaternion.identity);
        health -= player.GetComponent<PlayerCombat>().Attack;
    }

    void FixedUpdate()
    {
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 playerPos = player.transform.Find("Hitbox").gameObject.transform.position;

        yOffset = GetYOffset();

        //2d
        if (InputManager.Instance.is2D)
        {
            //if ((player.transform.position.x <= transform.position.x + xOffset && player.transform.position.x >= transform.position.x - xOffset) && (player.transform.position.z <= transform.position.z + zOffset && player.transform.position.z >= transform.position.z - zOffset))
            //if (Mathf.Abs(playerPos.x - enemyPos.x) < xOffset && Mathf.Abs(playerPos.z - (enemyPos.z + yOffset)) < zOffset)
			if (Mathf.Abs(playerPos.x - enemyPos.x) < xOffset && Mathf.Abs(playerPos.z - (enemyPos.z + zCol)) < zOffset)
			{
			//if attacking
				//Debug.Log("in collider");
            	//if (playerScript.melee == true && Input.GetKeyDown(KeyCode.Mouse1))
				if(player.GetComponent<PlayerAttack>().c.enabled)
            	{
					//Debug.Log ("is attacking");
            	    TakeDamage();
            	}
            }
        }

		if (health <= 0 && !isBoss)
        {
            
			int num = Random.Range (1, dropRate);
			//Debug.Log (num);
			if (num == 1) {
				Instantiate (itemToDrop, new Vector3(transform.position.x,1,transform.position.z), Quaternion.identity);
			}
			Instantiate(hitPart, transform.position, Quaternion.identity);
			Destroy (this.transform.parent.gameObject);
			Destroy (this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
		// visually had to  add 1.5 to gizmo
        //Vector3 gizmosPos = new Vector3(this.transform.parent.transform.position.x, 0, this.transform.parent.transform.position.z + yOffset);
		Vector3 gizmosPos = new Vector3(this.transform.parent.transform.position.x, 1, this.transform.parent.transform.position.z + zCol);
        Vector3 gizmosSize = new Vector3(xOffset, 1, zOffset);
        Gizmos.DrawWireCube(gizmosPos, gizmosSize);
    }

    float GetYOffset()
    {
        float a = 0;

        float angleB = 180 - (Camera.main.transform.eulerAngles.x + 90);
        angleB = Mathf.Round(angleB);
        Vector3 tmp = new Vector3(angleB, 0, 0);

		a = Mathf.Tan((tmp.x * Mathf.PI) / 180) * (this.transform.parent.transform.position.y);
		//a += 1.5f;
        return a;
    }

	public void GetCollider()
	{
		// 2D camera angle is 40
		//zCol = transform.localPosition.y * Mathf.Tan(40);
		collider.transform.position = new Vector3(transform.position.x, 1,zCol + transform.position.z);
	}
}
