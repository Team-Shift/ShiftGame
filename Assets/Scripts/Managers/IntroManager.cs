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
		txtList = new string[8] { "Help! Please help my village!",
            "Follow me. Only you can save us!" ,
			"The evil witch has taken the 3rd dimmension from us.\n" +
			"Press[shift]to switch to 3D and find a way over the gap.",
            "There's a wall in our way! Press [shift] again\nto change the world back to 2D.",
            "Help! It one of the lost souls. Take this sword.",
            "Click [left mouse] to attack and release the villagers soul.",
            "Thanks! This way to the village.",
            "Press '1' or '2' to use potion." };

		txt.text = "Help! Please help my village!";
        txt.color = Color.white;

        player = GameObject.FindGameObjectWithTag("Player");
		positionToFreeze = player.transform.position;
		freezePos = true;
        cam.SetActive(false);
        cam.SetActive(true);
        anim = canvas.GetComponent<Animator>();

        //
        HUD = GameObject.Find("InventorySlots");
	    invHUD = HUD.GetComponentInChildren<InvHUD>().gameObject;
        HUD.SetActive(false);
		count = 0;
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
        cam.SetActive(true);
        //Debug.Log("toSlides");
		freezePos = true;
		positionToFreeze = player.transform.position;
        //changeText();
        tutText.SetActive(false);
        
        anim.SetBool("FadeOut", false);
        if(count > 4)
        {
            HUD.SetActive(false);
        }
    }

    public void ToGame()
    {
        if (count != 7)
        {
            cam.SetActive(false);
            freezePos = false;
            tutText.SetActive(true);
            if (count > 4)
            {
                HUD.SetActive(true);
            }
        }
        else
        {
            //Destroy(player);
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
        //Debug.Log(player.GetComponent<Inventory>().invItems.Length);
        player.GetComponent<Inventory>().invItems[index] = temp; 
    }

    public void EnableHUD()
    {
        HUD.SetActive(true);
        player.GetComponent<Inventory>().InvUI = invHUD.GetComponent<InvHUD>();
    }

}
