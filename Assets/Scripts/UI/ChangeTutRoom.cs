using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
		//Debug.Log(other.name);
        if (other.tag == "Player")
        {
			if (intMan.count >= 6) {
				SceneManager.LoadScene ("FinalTown");
			} else {
				other.transform.position = exitPortal.transform.position;
				Debug.Log (other.name);
			}
            //intMan.ToSlides();
        }
		else if (other.tag == "NPC") {
			Debug.Log ("destroy!!!");
			Destroy (other.gameObject);
		}
    }
}
