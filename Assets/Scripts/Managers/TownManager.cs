using UnityEngine;
using System.Collections;

public class TownManager : MonoBehaviour {

	public static int deathCount = 0;
	public GameObject NCPvillager;
	GameObject townTxt;
	GUIText g_text;

	public string[] txtList;
	int txtIndex = 0;

	// Use this for initialization
	void Start () {
		townTxt = GameObject.Find ("Textbox");
		g_text =  townTxt.GetComponentInChildren<GUIText> ();

		// no cutscene stuff
		if (deathCount >= 1) {
			NCPvillager.SetActive (false);
			townTxt.SetActive (false);
		} else {
			txtList = new string[5] {"Welcome to my village.", 
				"Follow me to my house so I can give you something.",
				"Take this potion and press [1] or [2] to heal yourself.",
				"Aaaaahhhhhh!",
				"OOOOOOOOO"
			};
			g_text.text = txtList [txtIndex];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (deathCount < 1) {
			// do cut scene stuff
		}
	}

	public void IncreaseDeathCount()
	{
		deathCount++;
	}

	public void changetext()
	{
		txtIndex++;
		if (txtIndex > txtList.Length) {
			txtIndex = txtList.Length-1;
		}
		g_text.text = txtList [txtIndex];
	}
}
