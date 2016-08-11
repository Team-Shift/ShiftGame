using UnityEngine;
using System.Collections;

public class NPCwalk : MonoBehaviour {
	public  GameObject textBox;
	GUIText txt;
	public string thingToSay;
	TownManager manager;
	Animator anim;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("TownCutSceneManager").GetComponent<TownManager> ();
		if (manager.getDeathCount () > 0) {
			gameObject.GetComponent<Animator> ().SetBool ("shouldWalk", true);
		} else {
			gameObject.GetComponent<Wander> ().enabled = false;
		}
		anim = gameObject.GetComponent<Animator> ();
		txt = textBox.GetComponentInChildren<GUIText> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && manager.getDeathCount () > 0) {
			if (thingToSay != null) {
				textBox.SetActive (true);
				txt.text = thingToSay;
				anim.SetBool ("shouldWalk", false);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && manager.getDeathCount () > 0) {
			textBox.SetActive (false);
			anim.SetBool ("shouldWalk", true);
		}
	}
}
