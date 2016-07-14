using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject Manager;
    IntroManager intMan;

    public GameObject[] slides;
    public Animator anim;

    int currentSlide = 0;

    void Start()
    {
        intMan = FindObjectOfType<IntroManager>();
        for(int i=0; i < slides.Length; i++)
        {
            slides[i].SetActive (false);
        }
        slides[currentSlide].SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            ChangeSlide(true);
        }
    }

    public void ChangeSlide(bool direction)
    {
        anim.SetBool("FadeOut", true);
        StartCoroutine (SlideWait(direction));
    }

    public IEnumerator SlideWait(bool direction)
    {
        yield return new WaitForSeconds (1);
        slides[currentSlide].SetActive(false);

        intMan.ToGame();

        if (direction == true)
        {
            currentSlide += 1;
            if (currentSlide == slides.Length)
            {
                intMan.EnableHUD();
                SceneManager.LoadScene("FinalTown");
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
        //anim.SetBool("FadeOut", false);
        yield return true;
    }
}
