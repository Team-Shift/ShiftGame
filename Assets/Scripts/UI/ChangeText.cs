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
		if (other.tag == "Player") {
			if (isTown) {
				townMan.changetext ();
			} else if (!isTown) {
				intMan.changeText ();
			}
        
			Destroy (gameObject);
		}
	}
}
