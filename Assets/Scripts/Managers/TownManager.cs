using UnityEngine;
using System.Collections;

public class TownManager : MonoBehaviour {

	public static int deathCount = -1;
	public GameObject NPCvillager;
	public GameObject NPCghost;
	Light ghostFlash;
	float ghostSpeed;
	GameObject townTxt;
	GUIText g_text;

	public string[] txtList;
	public int txtIndex = 0;

	// Use this for initialization
	void Start () {
		townTxt = GameObject.Find ("Textbox");
		g_text =  townTxt.GetComponentInChildren<GUIText> ();
		ghostSpeed = NPCghost.GetComponent<Wander> ().speed;
		ghostFlash = NPCghost.GetComponent<Light> ();
		deathCount++;
		// no cutscene stuff
		if (deathCount >= 1) {
			NPCvillager.SetActive (false);
			NPCghost.SetActive (false);
			townTxt.SetActive (false);
		} else {
			txtList = new string[5] {"Welcome to my village.", 
				"Follow me to my house so I can give you something.",
				"Take this potion and press [1] or [2] to heal yourself.",
				"Aaaaahhhhhh!",
				"Help!"
			};
			g_text.text = txtList [txtIndex];
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	// put on timer 
	public void VillagerToGhost()
	{
		Destroy (NPCvillager);
		NPCghost.SetActive (true);
		NPCghost.GetComponent<Wander> ().enabled = false;
		StartCoroutine (ChangeTextTimer (3.5f));
	}

	public int getDeathCount()
	{
		return deathCount;
	}

	// sorry for the horrible naming 
	public void changetext()
	{
		txtIndex++;
		if (txtIndex > txtList.Length) {
			txtIndex = txtList.Length-1;
		}
		g_text.text = txtList [txtIndex];
	}

	public IEnumerator ChangeTextTimer(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		changetext();
		NPCghost.GetComponent<Wander> ().enabled = true;
		//NPCghost.GetComponent<Wander> ().speed = 3;
		//NPCghost.GetComponent<Wander> ().shouldWander = true; 
		//NPCghost.GetComponent<Animator> ().SetBool ("shouldGlide", true);
	}
}
