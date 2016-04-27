using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bank : MonoBehaviour {
    
    public List<ItemLibrary.ItemData> bankList;

    // Use this for initialization
    void Start () {
        bankList = new List<ItemLibrary.ItemData>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // switches inv and bank item
    void RetrieveItem(int id)
    {
        // get players inv
        Inventory playerInv = GameObject.FindWithTag("Player").GetComponent<Inventory>();

        // get index/itemType
        int index = (int)bankList[id].itemType;

        // create temp item (item coming from bank)
        Inventory.s_Items temp = new Inventory.s_Items();
        temp.item.ID = id;
        temp.item.itemName = bankList[id].name;
        temp.item.canPickup = false;
        temp.item.reccentlyPickupUp = false;
        temp.item.itype = bankList[id].itemType;

        // item in inventory to bank
        AddItem(playerInv.invItems[index].item.ID);

        // add temp item to inventory
        playerInv.invItems[index] = temp;
    }

    public void AddItem(int id)
    {
        // access db
        ItemLibrary itemDB = new ItemLibrary();
        // add correct item 
        bankList.Add(itemDB.allItems[id]);
    }
}
