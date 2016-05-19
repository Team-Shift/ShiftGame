using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {
	
    public GameObject tutText;
	public GameObject player;
	public GameObject cam;
	public int count;
	public string[] txtList;
	public bool freezePos;
    public GameObject canvas;
    public GameObject HUD;
    public GameObject invHUD;

    private Animator anim;
	private Vector3 positionToFreeze;

    GUIText txt;

	// Use this for initialization
	void Start () {
        txt = tutText.GetComponentInChildren<GUIText>();

        count = 0;
		txtList = new string[8] { "Use WASD to move.",
            "Press [shift] to change perspective. Move the mouse to look around." ,
            "Shift to 3D and press [space] to jump.",
            "Switch perspectives to raise and lower platforms.",
            "Pick up the sword.",
            "Click left mouse to attack.",
            "Pickup the potion.",
            "Press '1' or '2' to use potion." };

        txt.text = "WASD to move";
        txt.color = Color.white;

		positionToFreeze = player.transform.position;
		freezePos = true;
        cam.active = false;
        cam.active = true;
        anim = canvas.GetComponent<Animator>(); 
	}

	void Update()
	{
		if (freezePos) 
		{
			player.transform.position = positionToFreeze;
		}
	}

    void OnTriggerEnter()
    {
        player.transform.position = positionToFreeze;
    }

    public void ToSlides()
    {
        cam.active = true;
        Debug.Log("toSlides");
		freezePos = true;
		positionToFreeze = player.transform.position;
        changeText();
        tutText.active = false;
        
        anim.SetBool("FadeOut", false);
        if(count > 4)
        {
            HUD.active = false;
        }
    }

    public void ToGame()
    {
        if (count != 7)
        {
            cam.active = false;
            freezePos = false;
            tutText.active = true;
            if (count > 4)
            {
                HUD.active = true;
            }
        }
        else
        {
            Destroy(player);
        }
    }

    public void changeText()
    {
        if (count < txtList.Length - 1)
        {
            count++;
        }
        txt.text = txtList[count];
    }

    public void AddItemToInv(Item itemToAdd, int index)
    {
        Inventory.s_Items temp = new Inventory.s_Items();
        temp.item = itemToAdd;
        temp.quantity = 1;
        Debug.Log(player.GetComponent<Inventory>().invItems.Length);
        player.GetComponent<Inventory>().invItems[index] = temp; 
    }

    public void EnableHUD()
    {
        HUD.active = true;
        player.GetComponent<Inventory>().InvUI = invHUD.GetComponent<InvHUD>();
    }

}
