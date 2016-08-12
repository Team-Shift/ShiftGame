using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject Manager;
    IntroManager intMan;

    public GameObject[] slides;
    public Animator anim;
	bool canChange;

    int currentSlide = 0;

    void Start()
    {
        intMan = FindObjectOfType<IntroManager>();
        for(int i=0; i < slides.Length; i++)
        {
            slides[i].SetActive (false);
        }
        slides[currentSlide].SetActive(true);
		canChange = true;
    }

    void Update()
    {
		if(Input.GetButton("Fire1") && canChange)
        {
            ChangeSlide(true);
        }
    }

    public void ChangeSlide(bool direction)
    {
		canChange = false;
        anim.SetBool("FadeOut", true);
        StartCoroutine (SlideWait(direction));
    }

    public IEnumerator SlideWait(bool direction)
    {
        yield return new WaitForSeconds (1);
        //slides[currentSlide].SetActive(false);

        //intMan.ToGame();
		intMan.ToSlides();

        if (direction == true)
        {
			slides [currentSlide].SetActive (false);
            currentSlide += 1;
            if (currentSlide == slides.Length)
            {
                //intMan.EnableHUD();
				intMan.ToGame();
                //SceneManager.LoadScene("FinalTown");
                yield break;
                //currentSlide = 0;
            }
        }
        else if (direction == false)
        {
            currentSlide -= 1;
            if (currentSlide == -1)
            {
                currentSlide = slides.Length - 1;
            }
        }
        slides[currentSlide].SetActive(true);
		canChange = true;
        //anim.SetBool("FadeOut", false);
        yield return true;
    }
}
