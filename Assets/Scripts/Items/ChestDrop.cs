﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestDrop : MonoBehaviour {

    Animator anim;
    public List<Item> unlockedItems;
    public string itemName;         // insert name if want specific item dropping

    public float ySpawn;
    private bool spawned;
	// Use this for initialization
	void Start ()
    {
        ySpawn = -1.3f;
        //ItemManager.UnlockAllItems();
        unlockedItems = ItemManager.GetUnlockedItems();
        spawned = false;
        anim = gameObject.GetComponent<Animator>();
	}

	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("canOpen", true);
        }
    }

    public void SpawnItem()
    {
        // choose random item
        if (string.IsNullOrEmpty(itemName))
        {
            itemName = unlockedItems[Random.Range(0, unlockedItems.Count - 1)].itemName;
        }
        
        // set floating parent
        //GameObject g = Resources.Load("itemFloating") as GameObject;
        
        GameObject g = GameObject.Instantiate(Resources.Load("itemFloating"), transform.position + new Vector3(0,0,-.5f), Quaternion.identity) as GameObject;
        Debug.Log(g);
        foreach (Transform t in g.GetComponentsInChildren<Transform>())
        {
            if (t.name == "item_Particle")
            {
                GameObject itemSpawned = ItemManager.SpawnItem(itemName, gameObject.transform.position + new Vector3(0, 0, -.5f));
                if(itemName == "Bow")
                {
                    itemSpawned.transform.Translate(new Vector3 (0, .3f, 0));
                }
                itemSpawned.transform.SetParent(t);
            }
        }
        spawned = true;
    }
}
