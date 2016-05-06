using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestDrop : MonoBehaviour {

    Animator anim;
    public List<Item> unlockedItems;
    public string itemName;         // insert name if want specific item dropping

    private bool spawned;
	// Use this for initialization
	void Start ()
    {
        ItemManager.UnlockAllItems();
        unlockedItems = ItemManager.GetUnlockedItems(Item.ItemType.CONSUMABLE);
        spawned = false;
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("canOpen", true);
            if (!spawned)
            {
                SpawnItem();
            }
        }
    }

    public void SpawnItem()
    {
        // choose random item
        if (itemName == null)
        {
            itemName = unlockedItems[Random.Range(0, unlockedItems.Count - 1)].itemName;
        }
        ItemManager.SpawnItem(itemName, gameObject.transform.position + new Vector3(0, 1, 0));
        // set floating parent
        //GameObject g = GameObject.Instantiate(floatingObject, transform.position, Quaternion.identity) as GameObject;

        //foreach (Transform t in g.GetComponentsInChildren<Transform>())
        //{
        //    if (t.name == "item_Particle")
        //    {
        //        spawned = true;
        //    }
        //}
    }
}
