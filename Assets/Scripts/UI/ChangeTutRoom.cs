using UnityEngine;
using System.Collections;

public class ChangeTutRoom : MonoBehaviour {
    //public GameObject cam;
    IntroManager intMan;
    public GameObject manager;
    public GameObject Player;
    public GameObject exitPortal;

	// Use this for initialization
	void Start () {
        intMan = manager.GetComponent<IntroManager>();

	}

    void OnTriggerEnter()
    {
        intMan.ToSlides();
        Player.transform.position = exitPortal.transform.position;

        //cam.active = true;
    }
}
