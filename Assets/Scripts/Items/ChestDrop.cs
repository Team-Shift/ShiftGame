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
		Debug.Log (gameObject.transform.eulerAngles.y);
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
		GameObject g = GameObject.Instantiate(Resources.Load("itemFloating"), gameObject.transform.position, Quaternion.identity) as GameObject;
		//Debug.Log(g);
		g.transform.Translate(new Vector3(0,0, -.75f));
		g.transform.RotateAround (transform.position, Vector3.up, transform.eulerAngles.y + 180);
		foreach (Transform t in g.GetComponentsInChildren<Transform>())
		{
			if (t.name == "item_Particle")
			{
				GameObject itemSpawned = ItemManager.SpawnItem(itemName, g.transform.position + new Vector3(0, .25f, 0));
				if (itemName == "Bow") {
					itemSpawned.transform.localRotation = Quaternion.Euler (new Vector3 (40, 80, 0));
					Debug.Log ("Bow");
				} else if (itemName == "ChickenBow") {
					itemSpawned.transform.localRotation = Quaternion.Euler (new Vector3 (30, 70, 180));
					Debug.Log ("Bow");
				} else {
					if (itemName != "Healing Potion") {
						itemSpawned.transform.localRotation = Quaternion.Euler (new Vector3 (270, 0, 0));
					}
				}
				itemSpawned.transform.SetParent(t);
			}
		}
		spawned = true;
	}
}