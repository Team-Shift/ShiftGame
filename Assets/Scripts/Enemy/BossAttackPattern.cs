using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossAttackPattern : MonoBehaviour {
	EnemyHealth bossHealth;
	SpawnEnemies spawning;
	bool spawnedTurrets;
	public BoxCollider col;
	bool shouldRotate;
	bool shouldDie;
	Animator anim;
	// Use this for initialization
	void Start () {
		bossHealth = gameObject.GetComponentInChildren<EnemyHealth> ();
		spawning = gameObject.GetComponent<SpawnEnemies> ();
		anim = gameObject.GetComponent<Animator> ();

		shouldRotate = true;
		spawnedTurrets = false;
		shouldDie = true;

		BossManager.OnBossDead += this.EndBossFight;
	}

	void Awake()
	{
		BossManager.OnBossDead += this.EndBossFight;
	}

	void DestroyAllEnemies()
	{
		//Destroy(this.transform.parent.gameObject);
		GetComponentInChildren<BoxCollider> ().enabled = false;
		GameObject [] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject g in enemies)
		{
			if (g.name != "ScarecrowBoss") {
				Destroy (g);
				//Destroy (g.transform.parent.gameObject);

			}
		}
		//Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		// if half health
		if (bossHealth.health < (bossHealth.startHealth / 2) && !spawnedTurrets) {
			spawning.SpawnTurrets ();
			spawnedTurrets = true;
		}
		if (bossHealth.health < 1 && shouldDie) {
			BossManager.EndBossFight ();
		}
	}

	public void EndBossFight()
	{
		shouldRotate = false;
		gameObject.GetComponent<Animator> ().SetTrigger ("Death");
		shouldDie = false;
		col.enabled = false;
		Instantiate (Resources.Load("CongratsText"));
		DestroyAllEnemies ();
		spawning.enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			//shouldRotate = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") {
			anim.SetTrigger ("Attack");
			if (shouldRotate) {
				gameObject.transform.LookAt (new Vector3 (other.gameObject.transform.position.x, transform.position.y, other.gameObject.transform.position.z), Vector3.up);
			}//Debug.Log ("looking at");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			//shouldRotate = true;
			//anim.SetBool ("shouldAttack", false);
		}
	}

	public void ResetRotation()
	{
		shouldRotate = true;
		gameObject.transform.rotation =  Quaternion.Euler( new Vector3 (0,180,0));
	}

	public void StopRotation()
	{
		shouldRotate = false;
	}

	public void StartRotation()
	{
		shouldRotate = true;
	}
}
