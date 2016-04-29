using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item  :  MonoBehaviour{

    [HideInInspector]
    public enum ItemType{WEAPON, ARMOR, ABILITY, CONSUMABLE, BOOK, GOLD};

    public int ID;
    public string itemName;
    [HideInInspector]
    public bool canPickup;
    public bool reccentlyPickupUp;
    public int cost;
    public ItemType itype;

    void Start()
    {
        canPickup = false;
        reccentlyPickupUp = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if (other.tag == "Player") {canPickup = true;}
        if (canPickup)
        {
            //canPickup = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) //make sure player is found
            {
                Inventory i = player.GetComponent<Inventory>();
                InvHUD hud = GameObject.FindObjectOfType<InvHUD>();

                i.AddItem(ItemManager.GetItem(itemName));
                hud.ChangeUIIcon(ItemManager.GetItem(itemName));

                // if weapon, its going to switch positions (add this)
                if (canPickup && itype != ItemType.WEAPON)
                {
                    ItemManager.DestroyItem(gameObject);
                }
            }
            else
                Debug.Log("player not found");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            canPickup = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
            reccentlyPickupUp = false;
        }
    }
}
