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
	}

    void OnTriggerEnter()
    {
		anim.SetBool("FadeOut", true);
		//StartCoroutine (WaitForFade());

		Player.transform.position = exitPortal.transform.position;
		intMan.ToSlides();
    }

	public IEnumerator WaitForFade()
	{
		yield return new WaitForSeconds (1);
		intMan.ToSlides();
		Player.transform.position = exitPortal.transform.position;
		anim.SetBool ("FadeOut", false);
	}
}
