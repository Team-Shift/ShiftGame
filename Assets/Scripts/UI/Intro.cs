using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public GameObject[] slides;
    public Animator anim;

    int currentSlide = 0;

    void Start()
    {
        for(int i=0; i < slides.Length; i++)
        {
            slides[i].SetActive (false);
        }
        slides[currentSlide].SetActive(true);
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
        if (direction == true)
        {
            currentSlide += 1;
            if (currentSlide == slides.Length)
            {
                currentSlide = 0;
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
        anim.SetBool("FadeOut", false);
        yield return true;
    }
}
