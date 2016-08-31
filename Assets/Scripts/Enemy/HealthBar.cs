using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	GameObject cam;
	GameObject player;
	EnemyHealth health;
	GameObject healthBar;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		health = GameObject.Find ("ScarecrowBoss").GetComponentInChildren<EnemyHealth>();
		healthBar = GameObject.Find ("BossHealthBar");
		BossManager.OnStart += this.startBossFight;
		gameObject.SetActive (false);
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Awake()
	{
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		health = GameObject.Find ("ScarecrowBoss").GetComponentInChildren<EnemyHealth>();
		healthBar = GameObject.Find ("BossHealthBar");
		BossManager.OnStart += this.startBossFight;
		//gameObject.SetActive (false);
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.Instance.is2D) {
			gameObject.transform.LookAt (cam.transform);
		}
		else{
			gameObject.transform.LookAt (player.transform);
		}
		//Debug.Log (health.health);
		healthBar.transform.localScale = new Vector3((health.health) , 1,1);
		if (health.health <= 0) {
			healthBar.transform.localScale = new Vector3(0 , 1,1);
		}
	}

	public void startBossFight()
	{
		gameObject.SetActive (true);
	}
}
