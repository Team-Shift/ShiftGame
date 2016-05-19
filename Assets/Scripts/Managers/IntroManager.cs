using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {
	
    public GameObject tutText;
	public GameObject player;
	public GameObject cam;
	public int count;
	public string[] txtList;
	public bool freezePos;

	private Vector3 positionToFreeze;

    GUIText txt;

	// Use this for initialization
	void Start () {
        txt = tutText.GetComponentInChildren<GUIText>();

        count = 0;
		txtList = new string[4] { "WASD to move", "press shift to change perspective" , "space to jump", "click left mouse to attack"};

        txt.text = "WASD to move";
        txt.color = Color.white;

		positionToFreeze = player.transform.position;
		freezePos = true;
	}

	void Update()
	{
		if (freezePos) 
		{
			player.transform.position = positionToFreeze;
		}
	}

    public void ToSlides()
    {
		freezePos = true;
		positionToFreeze = player.transform.position;
        changeText();
        tutText.active = false;
        cam.active = true;
    }

    public void ToGame()
    {
		cam.active = false;
		freezePos = false;
        tutText.active = true;
        changeText();
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
