using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// will be on player 
public class Inventory : MonoBehaviour {

    public struct eItems
    {
        public Item item;
        public int quantity;   // so items can stack
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

        //eItems things = new eItems();
        //things.quantity = 2;

        for(int e = 0; e < equippedItems.Count; e++)
        {
            if (equippedItems[e].item.name == i.name && equippedItems[e].quantity < itemMaxStack)
            {
                Debug.Log(equippedItems[e].quantity);
                Debug.Log("Item exist");
                doesExist = true;
                // if item type exists, increase quantity
                int test = equippedItems[e].quantity;
                var item = equippedItems[e];
                item.quantity = test + 1;
                equippedItems[e] = item;

                
                //increaseQuantity(equippedItems[e]);
                Debug.Log("increasing quantity of " + equippedItems[e].item.name);
            }
            else if (equippedItems[e].quantity >= itemMaxStack)
                Debug.Log("reach max stack");
        }

        if (!doesExist)
        {
            Debug.Log("Item doesnt exist");
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
