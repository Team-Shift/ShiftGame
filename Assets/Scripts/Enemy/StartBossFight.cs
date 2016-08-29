using UnityEngine;
using System.Collections;

public class StartBossFight : MonoBehaviour {

	GameObject player;
	public GameObject boss;
	Animator bossAnim;
	public GameObject textBox;

	BossManager man;
	// Use this for initialization
	void Start () {
		BossManager.OnStart += this.startBossFight;
		bossAnim = boss.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void Awake()
	{
		BossManager.OnStart += this.startBossFight;
		//Debug.Log ("adding an event");
	}

	public void startBossFight()
	{
		if (GetComponent<BoxCollider> ().enabled) {
			GetComponent<BoxCollider> ().enabled = false;
		} else {
			gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
		//Debug.Log ("trigger subscriber");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			//boss animations
			bossAnim.SetTrigger("ScarecrowOpening");
			player.GetComponent<Animator> ().SetFloat ("x", 0);
			player.GetComponent<Animator> ().SetFloat ("y", 0);
			player.GetComponent<Custom2DController> ().enabled = false;
			textBox.SetActive (true);
			StartCoroutine (waitForAnimation());
		}
	}

	IEnumerator waitForAnimation()
	{
		yield return new WaitForSeconds (10);
		BossManager.OnStart += this.startBossFight;
		BossManager.startBossFight ();
		player.GetComponent<Custom2DController> ().enabled = true;
		textBox.SetActive (false);
	}
}
