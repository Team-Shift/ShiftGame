using UnityEngine;
using System.Collections;

public class ChangeText : MonoBehaviour {
	public GameObject Manager;
	IntroManager intMan;
	// Use this for initialization
	void Start () {
		intMan = Manager.GetComponent<IntroManager> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			intMan.changeText ();
        
			Destroy (gameObject);
		}
	}
}
