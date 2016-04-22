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
    
    /* {WEAPON, ARMOR, ABILITY, CONSUMABLE1, CONSUMABLE2} */
    public s_Items[] invItems;

    public int invSize = 5;
    int itemMaxStack = 1;

    void Start()
    {
        invItems = new s_Items[invSize];

    }

    void OnMouseOver()
    {
        // show name of item possibly
    }
   /*
    public void AddItem(Item i)
    {
        if(i.itype == Item.ItemType.WEAPON && !i.reccentlyPickupUp)
        {
            //Debug.Log("replacing weapon");
            if(!i.reccentlyPickupUp)
                ReplaceWeapon(i);
        }

        bool doesExist = false;
        for(int e = 0; e < equippedItems.Count; e++)
        {
            // if item has same name
            if (equippedItems[e].item.ID == i.ID)
            {
                if (equippedItems[e].quantity < itemMaxStack)
                {
                    //Debug.Log(equippedItems[e].quantity + " of this kind of item");
                    
                    // if item type exists, increase quantity
                    int test = equippedItems[e].quantity;
                    var item = equippedItems[e];
                    item.quantity = test + 1;
                    equippedItems[e] = item;

                    //Debug.Log("increasing quantity of " + equippedItems[e].item.itemName);
                }
                // item max carry
                else
                {
                    //Debug.Log("reach max stack cannot pickup");
                    i.canPickup = false;
                }
                doesExist = true;
            }
        }

        if (!doesExist)
        {
            // create eItem
            s_Items temp = new s_Items();
            //Debug.Log("Item doesnt exist");
            if (equippedItems.Count < invSize)
            {
                temp.quantity = 1;
                temp.item = i;
                equippedItems.Add(temp);
                //Debug.Log("adding new item " + temp.item.itemName);
            }
            // no room in inventory
            else
            {
                i.canPickup = false;
                ItemSwap();
                i.canPickup = true;
            }
        }
    }*/

    public void AddItem(Item i)
    {
        Debug.Log(i.itype);
        // if weapon: swap 
        if (i.itype == Item.ItemType.WEAPON && !i.reccentlyPickupUp)
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
            if (i.itype == Item.ItemType.CONSUMABLE && invItems[(int)i.itype].quantity > 0)
            {
                if (invItems[4].quantity >= 1) i.canPickup = false; // change to switch consumables
                else invItems[4] = temp;
                
            }
            else
            {
                invItems[(int)i.itype] = temp;
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

            // pickup new weapon
            pickupItem.transform.parent = tParent;
            pickupItem.transform.position = new Vector3(tParent.position.x, tParent.position.y, tParent.position.z);
            pickupItem.transform.localRotation = Quaternion.Euler(new Vector3(270,0,0));
            pickupItem.reccentlyPickupUp = true;
        }
    }

}
