using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item  :  MonoBehaviour{

    [HideInInspector]
    public enum ItemType{WEAPON, ARMOR, ABILITY, CONSUMABLE, BOOK, GOLD};

    //public int ID;
    public Texture sprite;
    public string itemName;
    //[HideInInspector]
    public bool canPickup;
    public bool reccentlyPickupUp;
    public int cost;
    public ItemType itype;
    public GameObject prefab_txt;
    private GameObject text2Destroy;
    private GUIText g_text;
    public bool beingSold;

    private GameObject player;
    private Inventory playerInv;
    private InvHUD hud;

    void Start()
    {
        canPickup = false;
        player = GameObject.Find("Player");

        playerInv = player.GetComponent<Inventory>();
        hud = GameObject.FindObjectOfType<InvHUD>();

        //reccentlyPickupUp = true;
        //beingSold = false;
    }

    void OnMouseEnter()
    {
        g_text = prefab_txt.GetComponent<GUIText>();
        Debug.Log("mouse down");
        if (beingSold)
        {
            // switch 
            g_text.text = itemName + ": " + cost + " gold.";
            text2Destroy = Instantiate(prefab_txt, new Vector3(.5f, .5f, 1), Quaternion.identity) as GameObject;
        }
    }

    void OnMouseExit()
    {
        Destroy(text2Destroy);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("entered");
        if (other.tag == "Player") {canPickup = true;}
        if (canPickup && !reccentlyPickupUp)
        {
            if (beingSold)
            {
                // if enough gold decrement gold
                if ((playerInv.goldCount - cost) > 0)
                {
                    playerInv.goldCount -= cost;
                }
                else
                {
                    canPickup = false;
                    Debug.Log("not enough gold");
                }
            }
            if (canPickup)
            {
                playerInv.AddItem(this);
                //playerInv.AddItem(ItemManager.GetItem(itemName));
                hud.ChangeUIIcon(ItemManager.GetItem(itemName));
                // mark item as unlocked/ add to drop list
                ItemManager.UnlockItem(this);
                ItemManager.DestroyItem(gameObject);
                reccentlyPickupUp = true;
                // if item is floating
                DestroyParent();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
            reccentlyPickupUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
            reccentlyPickupUp = false;
        }
    }

    void DestroyParent()
    {
        Transform parent = gameObject.transform.parent;
        if (parent != null)
        {
            while (parent.transform.parent != null)
            {
                parent = parent.transform.parent;
                Debug.Log(parent.name);
            }
            Destroy(parent.gameObject);
        }
    }
}
