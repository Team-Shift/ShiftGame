using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public struct s_Items
    {
        public Item item;
        public int quantity;   // so items can stack
        // item type
    }
    
    // slots in inventory 
    List<s_Items> equippedItems;
    // differentiate between type of weapon

    public int invSize = 4;
    int itemMaxStack = 1;

    void Start()
    {
        equippedItems = new List<s_Items>();
    }

    void OnMouseOver()
    {
        // show name of item
    }
   
    public void AddItem(Item i)
    {
        if(i.tag == "Weapon")
        {
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
                    Debug.Log(equippedItems[e].quantity + " of this kind of item");
                    
                    // if item type exists, increase quantity
                    int test = equippedItems[e].quantity;
                    var item = equippedItems[e];
                    item.quantity = test + 1;
                    equippedItems[e] = item;

                    Debug.Log("increasing quantity of " + equippedItems[e].item.itemName);
                }
                // item max carry
                else
                {
                    Debug.Log("reach max stack cannot pickup");
                    i.canPickup = false;
                }
                doesExist = true;
            }
        }

        if (!doesExist)
        {
            // create eItem
            s_Items temp = new s_Items();
            Debug.Log("Item doesnt exist");
            if (equippedItems.Count < invSize)
            {
                temp.quantity = 1;
                temp.item = i;
                equippedItems.Add(temp);
                Debug.Log("adding new item " + temp.item.itemName);
            }
            // no room in inventory
            else
            {
                i.canPickup = false;
                ItemSwap();
                i.canPickup = true;
            }
        }
    }

    void ReplaceWeapon(Item i)
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Weapon");
        foreach(Transform t in temp.GetComponentsInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }

        // access library and get weapon needed
        //GameObject weaponToAdd = ;
    }

    void ItemSwap()
    {
        Debug.Log("replacing");
    }

}
