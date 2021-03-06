﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item  :  MonoBehaviour{

    [HideInInspector]
    public enum ItemType{WEAPON, ARMOR, ABILITY, CONSUMABLE, BOOK, GOLD};

    //public int ID;
    public Texture sprite;
    public string itemName;
    //[HideInInspector]
    public bool canPickup;
    public bool reccentlyPickupUp;
    public int cost;
    public ItemType itype;
    public GameObject prefab_txt;
    private GameObject text2Destroy;
    private GUIText g_text;
    public bool beingSold;

    private GameObject player;
    private Inventory playerInv;
    private InvHUD hud;

    void Start()
    {
        canPickup = false;
        player = GameObject.FindGameObjectWithTag("Player");

        playerInv = player.GetComponent<Inventory>();
        hud = GameObject.FindObjectOfType<InvHUD>();

        //reccentlyPickupUp = true;
        //beingSold = false;
    }

    void OnMouseEnter()
    {
        //Debug.Log("selling " + this.itemName);
        if (beingSold)
        {
            g_text = prefab_txt.GetComponent<GUIText>();
            // switch 
            g_text.text = itemName + ": " + cost + " gold.";
            g_text.color = Color.black;
            g_text.fontSize = 20;
            text2Destroy = Instantiate(prefab_txt, new Vector3(.5f, .5f, 1), Quaternion.identity) as GameObject;
        }
    }

    void OnMouseExit()
    {
        Destroy(text2Destroy);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entered");
        if (other.tag == "Player") {canPickup = true;}
        if (canPickup && !reccentlyPickupUp)
        {
            if (beingSold)
            {
                // if enough gold decrement gold
                if ((playerInv.goldCount - cost) > 0)
                {
                    playerInv.goldCount -= cost;
                }
                else
                {
                    canPickup = false;
                    Debug.Log("not enough gold");
                }
            }
            if (canPickup)
            {
				if (!playerInv) {
					Debug.Log (playerInv);
					Debug.Log (player);
					player = GameObject.FindGameObjectWithTag ("Player");
					playerInv = player.GetComponent<Inventory>();
				}
                playerInv.AddItem(this);

                //playerInv.AddItem(ItemManager.GetItem(itemName));
                //Debug.Log(hud);
                if(hud == null)
                {
                    hud = GameObject.FindObjectOfType<InvHUD>();
                }
                hud.ChangeUIIcon(ItemManager.GetItem(itemName));
                
                // mark item as unlocked/ add to drop list
                //ItemManager.UnlockItem(this);
                ItemManager.DestroyItem(gameObject);
                reccentlyPickupUp = true;
                // if item is floating
                DestroyParent();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
            reccentlyPickupUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
            reccentlyPickupUp = false;
        }
    }

    void DestroyParent()
    {
        if (gameObject.transform.parent != null)
        {
            Transform parent = gameObject.transform.parent;
            while (parent.transform.parent != null && parent.transform.parent.name != "itemFloating(Clone)" && parent.transform.parent.name != "itemFloating")
            {
                parent = parent.transform.parent;
                //Debug.Log(parent.name);
            }
            Destroy(parent.gameObject);
        }
    }
}
