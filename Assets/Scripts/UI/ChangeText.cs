using UnityEngine;
using System.Collections;

public class ChangeText : MonoBehaviour {
	public GameObject Manager;
	IntroManager intMan;
	TownManager townMan;

	public bool isTown;
	// Use this for initialization
	void Start () {
		if (isTown) {
			townMan = Manager.GetComponent<TownManager> ();
		} else {
			intMan = Manager.GetComponent<IntroManager> ();
		}

	}
	
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("entered change text");
		if (other.tag == "Player") {
			if (isTown) {
				townMan.changetext ();
				//Debug.Log ("changed text");
			} else if (!isTown) {
				intMan.changeText ();
			}
        
			//Destroy (gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
