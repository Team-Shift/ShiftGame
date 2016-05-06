using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : MonoBehaviour
{


    public List<GameObject> StorageLocations;

	// Use this for initialization
	void Start () {
        //ToDo For Debug remove later
        //ItemManager.BankItemSet(Item.ItemType.CONSUMABLE);

        StorageLocations = new List<GameObject>();

        foreach (Transform child in transform)
	    {
            Debug.Log(child.name);
	        if (child.CompareTag("BankSlot"))
	        {
                StorageLocations.Add(child.gameObject);
            }
	    }

        LoadBankedItems();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadBankedItems()
    {
        //Might look for a better way to keep track of index
        int i = 0;
        foreach (var item in ItemManager.BankedItems)
        {
            GameObject g = ItemManager.SpawnItem(item.Key, StorageLocations[i].transform.position);
            g.transform.SetParent(StorageLocations[i].transform);
            i++;
        }
    }
}