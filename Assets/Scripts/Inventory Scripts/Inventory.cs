using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    [System.Serializable]
    public struct s_Items
    {
        public Item item;
        public int quantity;   // so items can stack
    }
    
    /* {[0]WEAPON, [1]ARMOR, [2]ABILITY, [3]CONSUMABLE1, [4]CONSUMABLE2, [5]BOOK} */
    //[HideInInspector]
    public s_Items[] invItems;

    [HideInInspector]
    public int invSize = 6;

    [HideInInspector]
    int itemMaxStack = 1;

    //[HideInInspector]
    public int goldCount;

    [HideInInspector]
    public int goldPickupAmnt = 5;

    [HideInInspector]
    public float ySpawn;

    public GameObject floatingObject;
    public InvHUD InvUI;

    void Start()
    {
        invItems = new s_Items[invSize];
        goldCount = 0;
        ySpawn = -1.3f;    // ground level
        if (GameObject.Find("ItemIcons").GetComponent<InvHUD>())
        {
            InvUI = GameObject.Find("ItemIcons").GetComponent<InvHUD>();
        }
        
        GameObject weaponLoc = GameObject.FindGameObjectWithTag("Weapon");
        //Debug.Log(weaponLoc.name);
        // get weapon transform
        foreach (Transform tCurrWeap in weaponLoc.GetComponentInChildren<Transform>())
        {
            //Debug.Log("adding cur weap");
            //Item i = tCurrWeap.gameObject.GetComponent<Item>();
            Item i = ItemManager.GetItem("WoodSword");
            //Debug.Log(i.itemName);
            s_Items newWeapon = new s_Items();
            newWeapon.item = i;
            newWeapon.quantity = 1;
            invItems[0] = newWeapon;
            //Debug.Log(newWeapon.item.itemName);
            //(invItems[0].item as iEquipable).OnUse(gameObject);
        }
    }

    void Update()
    {
        // switch consumables
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            s_Items temp = invItems[3];
            invItems[3] = invItems[4];
            invItems[4] = temp;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && invItems[3].item != null)
        {
            // get from  DB
            Item i = ItemManager.GetItem(invItems[3].item.itemName);
            (i as iConsumable).OnUse(gameObject);

            // delete item from inv
            invItems[3].item = null;
            invItems[3].quantity--;

            // remove item on UI
            InvUI.inv[3].GetComponent<GUITexture>().texture = null;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && invItems[4].item != null)
        {
            Item i = ItemManager.GetItem(invItems[4].item.itemName);
            (i as iConsumable).OnUse(gameObject);

            // delete item from inv
            invItems[4].item = null;
            invItems[4].quantity--;

            // remove item on UI
            InvUI.inv[4].GetComponent<GUITexture>().texture = null;
        }

            // gold hack
            if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
        {
            goldCount = 99999;
        }
        
    }

    void OnMouseOver()
    {
        // show name of item possibly
    }
  
    public void AddItem(Item i)
    {
        switch(i.itype)
        {
            case Item.ItemType.BOOK:
                // unlock book lore
                break;

            case Item.ItemType.GOLD:
                goldCount += goldPickupAmnt;
                break;

            case Item.ItemType.WEAPON:
                if (!i.reccentlyPickupUp)
                {
                    //Debug.Log(i.reccentlyPickupUp);

                    // instantiate game object
                    ReplaceWeapon(i);

                    // store in inventory
                    s_Items newWeapon = new s_Items();
                    newWeapon.item = ItemManager.GetItem(i.itemName);
                    newWeapon.quantity = 1;
                    invItems[0] = newWeapon;

                    // IF THIS FAILS, SOMETHING BAD GOT IN HERE
                    Debug.Assert((i is iEquipable));

                    (i as iEquipable).OnUse(gameObject);
                }
                break;

            case Item.ItemType.ARMOR:
                // store in inventory
                s_Items newArmor = new s_Items();
                newArmor.item = i;
                newArmor.quantity = 1;
                invItems[1] = newArmor;
                
                //(i as iEquipable).OnUse(gameObject);
                break;

            case Item.ItemType.ABILITY:
                // store in inventory
                s_Items newAbility = new s_Items();
                newAbility.item = i;
                newAbility.quantity = 1;
                invItems[2] = newAbility;

                //(i as iEquipable).OnUse(gameObject);
                break;

            case Item.ItemType.CONSUMABLE:
                s_Items temp = new s_Items();

                temp.item = ItemManager.GetItem(i.itemName);
                temp.quantity = 1;
                if (invItems[3].quantity == 0)
                {
                    invItems[3] = temp;
                }
                else // replace
                {
                    if (invItems[4].quantity > 0 && invItems[3].quantity > 0)
                    {
                        //Debug.Log(invItems[4].item.itemName);
                        // ***** put parent on locator *****
                        GameObject g = ItemManager.SpawnItem(invItems[4].item.itemName, new Vector3(i.transform.position.x, gameObject.transform.position.y, i.transform.position.z));
                        SetFloatingParent(g);

                        g.GetComponent<Item>().reccentlyPickupUp = true;
                    }
                    invItems[4] = temp;
                }
                break;
        }

        // if item exists, ids match, !maxstack: increase quantity
        if (invItems[(int)i.itype].item != null && i.itemName == invItems[(int)i.itype].item.itemName && invItems[(int)i.itype].quantity < itemMaxStack)
        {
            invItems[(int)i.itype].quantity++;
        }

        // if max stack: dont pickup
        else if (invItems[(int)i.itype].quantity >= itemMaxStack && i.itype != Item.ItemType.CONSUMABLE)  i.canPickup = false; 
    }

    void ReplaceWeapon(Item pickupItem)
    {
        // find locator/parent for weapon
        GameObject weaponLoc = GameObject.FindGameObjectWithTag("Weapon");
        //Debug.Log(weaponLoc.name);
        // delete current weapon
        foreach (Transform tCurrWeap in weaponLoc.GetComponentInChildren<Transform>())
        {
            Destroy(tCurrWeap.gameObject);
        }
        //Transform tCurrWeap = weaponLoc.GetComponentInChildren<Transform>();

        // spawn the current weapon
        GameObject dropWeap  = ItemManager.SpawnItem(invItems[0].item.itemName, new Vector3(pickupItem.transform.position.x, gameObject.transform.position.y + 0.2f, pickupItem.transform.position.z));
        if (dropWeap.GetComponent<Item>().itemName != "Bow")
        {
            dropWeap.transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));
        }
        else
        {
            dropWeap.transform.position = new Vector3(pickupItem.transform.position.x, gameObject.transform.position.y , pickupItem.transform.position.z);
            //dropWeap.transform.Translate(new Vector3(0, .3f, 0));
        }
        dropWeap.GetComponent<Item>().reccentlyPickupUp = true;
        dropWeap.GetComponent<Collider>().enabled = true;
        SetFloatingParent(dropWeap);

        // pickup new weapon from DB
        GameObject pickUp = ItemManager.SpawnItem(pickupItem.itemName, weaponLoc.transform.position);
        pickUp.transform.SetParent(weaponLoc.transform);
        if(pickupItem.itemName == "Bow")
        {
            pickUp.transform.localRotation = Quaternion.Euler(new Vector3(30, 140, 180));
        }
        if (pickupItem.itemName == "ChickenBow")
        {
            pickUp.transform.localRotation = Quaternion.Euler(new Vector3(30, 70, 180));
        }
        else
            pickUp.transform.localRotation = Quaternion.Euler(new Vector3(270, 0, 0));
        pickUp.GetComponent<Item>().reccentlyPickupUp = true;

        pickUp.GetComponent<Collider>().enabled = false;
        
    }


    public void SetFloatingParent(GameObject item)
    {
        //Debug.Log("Setting parent");
        // prefab
        GameObject g = GameObject.Instantiate(floatingObject, new Vector3(item.transform.position.x, gameObject.transform.position.y, item.transform.position.z), Quaternion.identity) as GameObject;

        foreach(Transform t in g.GetComponentsInChildren<Transform>())
        {
            if(t.name == "item_Particle")
            {
                item.transform.SetParent(t);
            }
        }
    }
}
