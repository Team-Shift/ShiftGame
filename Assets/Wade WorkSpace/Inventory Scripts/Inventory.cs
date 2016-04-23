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

    void Start()
    {
        invItems = new s_Items[invSize];
        goldCount = 0;
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
        // gold hack
        if(Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
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
        Debug.Log(i.itype);
        // if book add to collection
        if(i.itype == Item.ItemType.BOOK)
        {
            // unlock book lore
        }
        else if(i.itype == Item.ItemType.GOLD)
        {
            goldCount += goldPickupAmnt;
        }
        // if weapon: swap 
        else if (i.itype == Item.ItemType.WEAPON && !i.reccentlyPickupUp)
        {
            if (!i.reccentlyPickupUp)
                ReplaceWeapon(i);
        }

        // if item exists, ids match, !maxstack: increase quantity
        if (invItems[(int)i.itype].item != null && i.ID == invItems[(int)i.itype].item.ID && invItems[(int)i.itype].quantity < itemMaxStack)
        {
            invItems[(int)i.itype].quantity++;
        }

        // if max stack: dont pickup
        else if (invItems[(int)i.itype].quantity >= itemMaxStack && i.itype != Item.ItemType.CONSUMABLE)  i.canPickup = false; 

        // else: new item ID
        else
        {
            // store new itemData in temp
            s_Items temp = new s_Items();
            temp.item = i;
            temp.quantity = 1;

            // if consumable: check both slots for availability
            if (i.itype == Item.ItemType.CONSUMABLE && invItems[(int)i.itype].quantity > 0 && invItems[4].quantity == 0)
            {
                invItems[4] = temp;
            }
            else
            {
                invItems[(int)i.itype] = temp;
                if (i.itype == Item.ItemType.CONSUMABLE) { /*DROP THE CURRENT CONSUMABLE*/ }
            }
        }
    }

    void ReplaceWeapon(Item pickupItem)
    {
        // find locator/parent for weapon
        GameObject weaponLoc = GameObject.FindGameObjectWithTag("Weapon");

        // get weapon transform
        foreach(Transform tCurrWeap in weaponLoc.transform)
        {
            Transform temp = tCurrWeap;
            Transform tParent = tCurrWeap.parent;

            // drop current weapon
            tCurrWeap.parent = pickupItem.transform.parent;
            tCurrWeap.position = pickupItem.transform.position;
            tCurrWeap.rotation = Quaternion.Euler(Vector3.zero);
            tCurrWeap.gameObject.GetComponent<Item>().reccentlyPickupUp = true;
            tCurrWeap.GetComponent<Collider>().enabled = true;

            // pickup new weapon
            pickupItem.transform.parent = tParent;
            pickupItem.transform.position = new Vector3(tParent.position.x, tParent.position.y, tParent.position.z);
            pickupItem.transform.localRotation = Quaternion.Euler(new Vector3(270,0,0));
            pickupItem.reccentlyPickupUp = true;
            pickupItem.GetComponent<Collider>().enabled = false;
        }
    }

}
