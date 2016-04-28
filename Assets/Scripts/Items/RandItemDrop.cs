using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandItemDrop : MonoBehaviour {

    List<GameObject> unlockedItems;
	// Use this for initialization
	void Start ()
    {
        unlockedItems = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter()
    {
        // store all unlocked items
        //foreach(GameObject g in ItemManager.GetUnlockedItems())
        //{
        //    unlockedItems.Add(g);
        //}
        // choose random item
        string itemName = unlockedItems[Random.Range(0, unlockedItems.Count - 1)].name;
        ItemManager.SpawnItem(itemName, gameObject.transform.position);
    }
}
