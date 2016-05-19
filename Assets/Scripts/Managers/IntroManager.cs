using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {
    public GameObject tutText;
    GUIText txt;
    public GameObject cam;
    public int count;
    public string[] txtList;

	// Use this for initialization
	void Start () {
        txt = tutText.GetComponentInChildren<GUIText>();

        count = 0;
        txtList = new string[4] { "WASD to move", "click left mouse to attack", "press shift to shift", "space to jump"};

        txt.text = "WASD to move";
        txt.color = Color.white;
	}

    public void ToSlides()
    {
        changeText();
        tutText.active = false;
        cam.active = true;
    }

    public void ToGame()
    {
        tutText.active = true;
        changeText();
        cam.active = false;
    }

    public void changeText()
    {
        txt.text = txtList[count];
        if (count <= 4)
        {
            count++;
        }
    }

}
