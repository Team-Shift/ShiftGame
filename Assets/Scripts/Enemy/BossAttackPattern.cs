using UnityEngine;
using System.Collections;

public class BossAttackPattern : MonoBehaviour {
	EnemyHealth bossHealth;
	SpawnEnemies spawning;
	bool spawnedTurrets;

	bool shouldRotate;

	Animator anim;
	// Use this for initialization
	void Start () {
		bossHealth = gameObject.GetComponentInChildren<EnemyHealth> ();
		spawning = gameObject.GetComponent<SpawnEnemies> ();
		anim = gameObject.GetComponent<Animator> ();

		shouldRotate = true;
		spawnedTurrets = false;
	}
	
	// Update is called once per frame
	void Update () {
		// if half health
		if (bossHealth.health < (bossHealth.startHealth / 2) && !spawnedTurrets) {
			spawning.SpawnTurrets ();
			spawnedTurrets = true;
		}
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
