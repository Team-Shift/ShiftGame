using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bank : MonoBehaviour {
    
    public List<Inventory.s_Items> bankList;

    // Use this for initialization
    void Start () {
        bankList = new List<Inventory.s_Items>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // switches inv and bank item
    void RetrieveItem(Item i)
    {
        // get players inv
        Inventory playerInv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        playerInv.AddItem(i);

        // item in inventory to bank
        AddItemToBank(playerInv.invItems[(int)i.itype].item.itemName);
    }

    public void AddItemToBank(string name)
    {
        Inventory.s_Items temp = new Inventory.s_Items();
        temp.item = ItemManager.GetItem(name);
        temp.quantity = 1;
        bankList.Add(temp);
    }
}
