using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	public GameObject npc;
	public bool needs2D;
	InputManager player;
	Wander movement;
	Animator anim;
	// Use this for initialization
	void Start () {
		if (needs2D) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<InputManager> ();
		}
		anim = npc.GetComponent<Animator> ();
		movement = npc.GetComponent<Wander> ();
		movement.speed = 0;
		//movement.shouldWander = false;
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player"){
			if (needs2D && player.is2D) {
				MoveNPC ();
			} else if (!needs2D) {
				MoveNPC ();
			}

		}
	}
	void MoveNPC()
	{
		movement.shouldWander = true;
		movement.speed = 3;
		anim.SetBool ("shouldWalk", true);
	}
}
