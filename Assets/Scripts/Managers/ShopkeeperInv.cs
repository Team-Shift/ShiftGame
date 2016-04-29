using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopkeeperInv : MonoBehaviour {

    // { [0]Weapon, [1]armor, [2]consumable, [3]consumable, [4]ability }
    public Item[]itemsForSale;
    // just call ItemManager.function()

    [HideInInspector]
    public int numItems = 5;
    GameObject player;
    public float resellPerc = 0.3f;

    // Use this for initialization
    void Start () {
        // create array
        itemsForSale = new Item[numItems];
        player = GameObject.FindGameObjectWithTag("Player");

        // populate list dependent on unlocked items
        GenerateInventory();
	}

    // call when player completes dungeon, dies, or teleports out
    void GenerateInventory()
    {
        //// get unlocked items and sort
        //List<Item> unlockedWeapons = ItemManager.GetUnlockedWeapons();
        //List<Item> unlockedArmor = ItemManager.GetUnlockedArmor();
        //List<Item> unlockedConsumables = ItemManager.GetUnlockedConsumables();
        //List<Item> unlockedAbilities = ItemManager.GetUnlockedAbilites();

        //// randomly choose items
        //itemsForSale[0] = unlockedWeapons[Random.Range(0, unlockedWeapons.Count-1)];
        //itemsForSale[1] = unlockedArmor[Random.Range(0, unlockedArmor.Count - 1)];
        //itemsForSale[2] = unlockedConsumables[Random.Range(0, unlockedWeapons.Count - 1)];
        //itemsForSale[3] = unlockedConsumables[Random.Range(0, unlockedWeapons.Count - 1)];
        //itemsForSale[4] = unlockedAbilities[Random.Range(0, unlockedAbilities.Count - 1)];
        //// have consumables for sale diferent
        //while(itemsForSale[3].ID == itemsForSale[4].ID)
        //{
        //    itemsForSale[4] = unlockedAbilities[Random.Range(0, unlockedAbilities.Count - 1)];
        //}
        // spawn itemsForSale (child of shopkeeper?)
        //Vector3 offset = Vector3.zero;
        //foreach (Item i in itemsForSale)
        //{
        //    ItemManager.SpawnItem(i.name, gameObject.transform.position + offset);
        //    offset.z += 1;
        //}
    }

    void PlayerBuyItem(int index)
    {
        Inventory inv = player.GetComponent<Inventory>();

        // can't item.canPickup = false until press 'Enter' && enough gold
        if ((inv.goldCount - itemsForSale[index].cost) >= 0)
        {
            // decrement gold
            inv.goldCount -= itemsForSale[index].cost;
            // change display
            player.GetComponent<InvHUD>().ReduceGold(itemsForSale[index].cost);
        }
        else Debug.Log("not enough money");

        // if slot available: add to inventory
        //inv.invItems[]

        // else replace w/current and add current to bank
    }

    int PlayerSellItem(string itemName)
    {
        //ItemManager.GetItemData(itemName).cost;
        int sellingPrice = (int)(/*(float)ItemManager.GetItemData(itemName).cost */ resellPerc);
        Debug.Log("sell for " + sellingPrice + "?");
        return sellingPrice;
    }
}
