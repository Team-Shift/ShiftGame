using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    [System.Serializable]
    public struct s_Items
    {
        public Item item;
        public int quantity;   // so items can stack
    }
    
    /* {[0]WEAPON, [1]ARMOR, [2]ABILITY, [3]CONSUMABLE1, [4]CONSUMABLE2, [5]BOOK} */
    public s_Items[] invItems;
    public int invSize = 6;
    int itemMaxStack = 1;

    public int goldCount;
    public int goldPickupAmnt = 5;

    InvHUD InvUI;

    void Start()
    {
        invItems = new s_Items[invSize];
        goldCount = 0;
        InvUI = GameObject.Find("ItemIcons").GetComponent<InvHUD>();
    }

    void Update()
    {
        // switch consumables
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            s_Items temp = invItems[3];
            invItems[3] = invItems[4];
            invItems[4] = temp;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && invItems[3].item != null)
        {
            // get from  DB
            Item i = ItemManager.GetItem(invItems[3].item.itemName);
            (i as iConsumable).OnUse(gameObject);

            // delete item from inv
            invItems[3].item = null;
            invItems[3].quantity--;

            // remove item on UI
            InvUI.inv[3].GetComponent<GUITexture>().texture = null;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && invItems[4].item != null)
        {
            Item i = ItemManager.GetItem(invItems[4].item.itemName);
            (i as iConsumable).OnUse(gameObject);

            // delete item from inv
            invItems[4].item = null;
            invItems[4].quantity--;

            // remove item on UI
            InvUI.inv[4].GetComponent<GUITexture>().texture = null;
        }

            // gold hack
            if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
        {
            goldCount = 99999;
        }
        
    }

    void OnMouseOver()
    {
        // show name of item possibly
    }
  
    public void AddItem(Item i)
    {
        switch(i.itype)
        {
            case Item.ItemType.BOOK:
                // unlock book lore
                break;

            case Item.ItemType.GOLD:
                goldCount += goldPickupAmnt;
                break;

            case Item.ItemType.WEAPON:
                if (!i.reccentlyPickupUp)
                {
                    Debug.Log(i.reccentlyPickupUp);
                    ReplaceWeapon(i);

                    // IF THIS FAILS, SOMETHING BAD GOT IN HERE
                    Debug.Assert((i is iEquipable));

                    (i as iEquipable).OnUse(gameObject);
                }
                break;
            case Item.ItemType.CONSUMABLE:
                s_Items temp = new s_Items();

                temp.item = i;
                temp.quantity = 1;
                if (invItems[3].quantity == 0)
                {
                    invItems[3] = temp;
                }
                else // replace
                {
                    if (invItems[4].quantity > 0 && invItems[3].quantity > 0)
                    {
                        //Debug.Log(invItems[4].item.itemName);
                        GameObject g = ItemManager.SpawnItem(invItems[4].item.itemName, transform.position);

                        g.GetComponent<Item>().reccentlyPickupUp = true;
                    }
                    invItems[4] = temp;
                }
                break;
        }

        // if item exists, ids match, !maxstack: increase quantity
        if (invItems[(int)i.itype].item != null && i.itemName == invItems[(int)i.itype].item.itemName && invItems[(int)i.itype].quantity < itemMaxStack)
        {
            invItems[(int)i.itype].quantity++;
        }

        // if max stack: dont pickup
        else if (invItems[(int)i.itype].quantity >= itemMaxStack && i.itype != Item.ItemType.CONSUMABLE)  i.canPickup = false; 
    }

    void ReplaceWeapon(Item pickupItem)
    {
        Debug.Log("Swapping Weapons");
        // find locator/parent for weapon
        GameObject weaponLoc = GameObject.FindGameObjectWithTag("Weapon");

        // get weapon transform
        foreach(Transform tCurrWeap in weaponLoc.transform)
        {
            // drop current weapon
            tCurrWeap.SetParent(null);
            tCurrWeap.position = transform.position;
            tCurrWeap.rotation = Quaternion.Euler(new Vector3(270,0,0));
            tCurrWeap.GetComponent<Item>().reccentlyPickupUp = true;
            tCurrWeap.GetComponent<Collider>().enabled = true;

            // pickup new weapon from DB
            GameObject g = ItemManager.SpawnItem(pickupItem.itemName, weaponLoc.transform.position);
            g.transform.SetParent(weaponLoc.transform);
            g.transform.localRotation = Quaternion.Euler(new Vector3(270,0,0));
            g.GetComponent<Item>().reccentlyPickupUp = true;

            g.GetComponent<Collider>().enabled = false;
        }
    }

}
