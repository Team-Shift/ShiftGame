using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandItemDrop : MonoBehaviour {

    Animator anim;
    List<GameObject> unlockedItems;
    public  bool canDrop;

	// Use this for initialization
	void Start ()
    {
        unlockedItems = new List<GameObject>();
        anim = GetComponent<Animator>();
        canDrop = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canDrop) 
        {
            canDrop = false;
            anim.SetBool("canOpen", true);

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
}
