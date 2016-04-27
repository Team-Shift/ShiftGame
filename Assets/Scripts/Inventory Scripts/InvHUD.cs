using UnityEngine;
using System.Collections;

public class InvHUD : MonoBehaviour {

    public GameObject[] inv;
	// Use this for initialization
	void Start () {
        inv = new GameObject[transform.childCount];
        for(int i =0; i < transform.childCount; i++)
        {
            inv[i] = transform.GetChild(i).gameObject;
        }
	}
	
    void FixedUpdate()
    {
        // switch consumables
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Texture temp = inv[3].GetComponent<GUITexture>().texture;
            inv[3].GetComponent<GUITexture>().texture = inv[4].GetComponent<GUITexture>().texture;
            inv[4].GetComponent<GUITexture>().texture = temp;
        }
    }

    public void ChangeUIIcon(Item i)
    {
        if(i.itype == Item.ItemType.GOLD)
        {
            // increase gold display
        }
        // if first consumable slot is taken
        else if (i.itype == Item.ItemType.CONSUMABLE && inv[(int)i.itype].GetComponent<GUITexture>().texture != null && inv[4].GetComponent<GUITexture>().texture == null)
        {
            inv[4].GetComponent<GUITexture>().texture = FindItemSprite(i.ID);
        }
        else
        {
            Debug.Log("change texture");
            inv[(int)i.itype].GetComponent<GUITexture>().texture = FindItemSprite(i.ID);
        }
    }

    public void ReduceGold(int amount)
    {
        // change gold display 
        // goldAMount -= amount;
    }

    Texture FindItemSprite(int i)
    {
        Texture temp;
        // CHANGE search db by itemID
        if (i == 0)
        {
            temp = Resources.Load<Texture>("ItemSprites/HealthPotionItem");
        }
        else if(i == 8)
        {
            temp = Resources.Load<Texture>("ItemSprites/GreenPotion");
        }
        else
            temp = Resources.Load<Texture>("ItemSprites/SwordIcon");
        return temp;
    }

}
