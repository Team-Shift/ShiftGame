using UnityEngine;
using System.Collections;

public class NPCCutscene : MonoBehaviour {
	public GameObject npc;
	Animator anim;
	Wander wanderNPC;
	TownManager manager;

	void Start()
	{
		anim = GetComponent<Animator> ();

		wanderNPC = npc.GetComponent<Wander>();
		//Debug.Log (npc.name);
		manager = GameObject.Find ("TownCutSceneManager").GetComponent<TownManager>();
	}

	void Update()
	{
		//Debug.Log (manager.txtIndex);

	}


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "NPC") {
			//Debug.Log ("enterd to stop");
			//Debug.Log (npc.GetComponent<Wander> ().shouldWander);
			wanderNPC.enabled = false;

			manager.changetext ();
			anim.SetBool ("shouldWalk", false);
			npc.transform.Rotate (new Vector3(0,90,0));

			// spawn item
			GameObject g = GameObject.Instantiate(Resources.Load("itemFloating"), gameObject.transform.position, Quaternion.identity) as GameObject;
			//Debug.Log(g);
			//g.transform.Translate(new Vector3(0,0, -.75f));
			g.transform.RotateAround (transform.position, Vector3.up, transform.eulerAngles.y + 180);

			GameObject itemSpawned = ItemManager.SpawnItem("Healing Potion", g.transform.position + new Vector3(0, .25f, 0));
			//itemSpawned.transform.localRotation = Quaternion.Euler (new Vector3 (270, 0, 0));
			foreach (Transform t in g.GetComponentsInChildren<Transform>()) {
				if (t.name == "item_Particle") {
					itemSpawned.transform.SetParent (t);
				}
			}

			//Destroy (gameObject);
		}
	}


	void OnTriggerExit(Collider col)
	{
		if (col.tag=="Player") {
			manager.changetext ();

			// add some kind of animation and put this on a timer
			manager.VillagerToGhost();
			Destroy (gameObject);
			//StartCoroutine(ActivateGhostOnTimer(3));
		}

	}

	IEnumerator ActivateGhostOnTimer(float time)
	{
		yield return new WaitForSeconds (time);
		manager.VillagerToGhost ();
	}
}
