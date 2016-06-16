using UnityEngine;
using System.Collections;

public class ChangeTutRoom : MonoBehaviour {
    //public GameObject cam;
    IntroManager intMan;
    public GameObject manager;
    public GameObject Player;
    public GameObject exitPortal;
	Animator anim;

	// Use this for initialization
	void Start () {
        intMan = manager.GetComponent<IntroManager>();
		anim = gameObject.GetComponent<Animator> ();
        Player = GameObject.FindWithTag("Player");
	}

    void OnTriggerEnter(Collider other)
    {
        //anim.SetBool("FadeOut", true);
        //StartCoroutine (WaitForFade());
        //Debug.Log("triggered");
        if (other.tag == "Player")
        {
            other.transform.position = exitPortal.transform.position;
            intMan.ToSlides();
        }
    }

}
