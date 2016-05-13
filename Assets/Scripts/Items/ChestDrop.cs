using UnityEngine;
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
        //Debug.Log(gameObject.name);
        GameObject g = GameObject.Instantiate(Resources.Load("itemFloating"), gameObject.transform.position + new Vector3(0,.5f,0), Quaternion.identity) as GameObject;
        //Debug.Log(g);
        foreach (Transform t in g.GetComponentsInChildren<Transform>())
        {
            if (t.name == "item_Particle")
            {
                GameObject itemSpawned = ItemManager.SpawnItem(itemName, gameObject.transform.position + new Vector3(0, .5f, 0));
                if(itemName == "Bow")
                {
                    itemSpawned.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .7f, gameObject.transform.position.z);
                    //itemSpawned.transform.Translate(new Vector3 (0, 0, 0));
                }
                itemSpawned.transform.SetParent(t);
            }
        }
        spawned = true;
    }
}
