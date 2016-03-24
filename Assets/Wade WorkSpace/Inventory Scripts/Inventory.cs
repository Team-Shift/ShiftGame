using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// will be on player 
public class Inventory : MonoBehaviour {

    public struct eItems
    {
        public Item item;
        public int quantity;    // so items can stack
    }
    
    // amount of slots in inventory 
    public List<eItems> equippedItems;
    //public Item[] equippedItems;
    int invSize = 4;
    int itemMaxStack = 5;

    void Start()
    {
        equippedItems = new List<eItems>();
    }

    void OnMouseOver()
    {
        // show name of item
    }
   
    public void AddItem(Item i)
    {
        // create eItem
        eItems temp = new eItems();
        bool doesExist = false;

        foreach(eItems e in equippedItems)
        {
            // not for weapons
            if(e.item.name == i.name && e.quantity < itemMaxStack)
            {
                doesExist = true;
                // if item type exists, increase quantity
                increaseQuantity(e);
                Debug.Log("increasing quantity of " + e.item.name);
            }
            if (e.quantity >= itemMaxStack)
                Debug.Log("reach max stack");
        }

        if (!doesExist)
        {
            if (equippedItems.Count < invSize)
            {
                temp.quantity = 1;
                temp.item = i;
                equippedItems.Add(temp);
                Debug.Log("adding new item " + temp.item.name);
            }
            else
                Replace();
        }
    }

    void increaseQuantity(eItems e)
    {
        Debug.Log(e.quantity);
        e.quantity++;
        Debug.Log(e.quantity);
    }

    void Replace()
    {
        Debug.Log("replacing");
    }

}
