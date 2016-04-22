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
	
    public void ChangeUIIcon(Item i)
    {
        // CHANGE to switch consumables if player wants to
        // if first consumable slot is taken
        if (i.itype == Item.ItemType.CONSUMABLE && inv[(int)i.itype].GetComponent<GUITexture>().texture.name != "InventorySlot 1")
        {
            inv[(int)i.itype + 1].GetComponent<GUITexture>().texture = FindItemSprite(i.ID);
        }
        else
            inv[(int)i.itype].GetComponent<GUITexture>().texture = FindItemSprite(i.ID);
    }

    Texture FindItemSprite(int i)
    {
        Texture temp;
        // CHANGE search db by itemID
        if (i == 0)
        {
            temp = Resources.Load<Texture>("ItemSprites/HealthPotionItem");
        }
        else
            temp = Resources.Load<Texture>("ItemSprites/SwordIcon");
        return temp;
    }

}
