using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopkeeperInv : MonoBehaviour {

    // { [0]Weapon, [1]armor, [2]consumable, [3]consumable, [4]ability }
    public List<Item>itemsForSale;
    // just call ItemManager.function()

    [HideInInspector]
    public int numItems = 5;
    GameObject player;
    public float resellPerc = 0.3f;

    public List<Item> unlockedWeapons;
    public List<Item> unlockedArmor;
    public List<Item> unlockedConsumables;
    public List<Item> unlockedAbilities;

    // Use this for initialization
    void Start () {
        // create array
        itemsForSale = new List<Item>();
        player = GameObject.FindGameObjectWithTag("Player");

        unlockedWeapons = new List<Item>();
        unlockedArmor = new List<Item>();
        unlockedConsumables = new List<Item>();
        unlockedAbilities = new List<Item>();

        ItemManager.UnlockAllItems();
        // populate list dependent on unlocked items
        GenerateInventory();
	}

    // call when player completes dungeon, dies, or teleports out
    void GenerateInventory()
    {
        itemsForSale.Clear();
        // get unlocked items and sort
        //Debug.Log(ItemManager.GetUnlockedItems(Item.ItemType.WEAPON));

        unlockedWeapons = ItemManager.GetUnlockedItems(Item.ItemType.WEAPON);
        unlockedArmor = ItemManager.GetUnlockedItems(Item.ItemType.ARMOR);
        unlockedConsumables = ItemManager.GetUnlockedItems(Item.ItemType.CONSUMABLE);
        unlockedAbilities = ItemManager.GetUnlockedItems(Item.ItemType.ABILITY);

        // randomly choose items
        itemsForSale.Add(unlockedWeapons[Random.Range(0, unlockedWeapons.Count - 1)]);
        itemsForSale.Add(unlockedArmor[Random.Range(0, unlockedArmor.Count - 1)]);
        itemsForSale.Add(unlockedConsumables[Random.Range(0, unlockedWeapons.Count - 1)]);
        itemsForSale.Add(unlockedConsumables[Random.Range(0, unlockedWeapons.Count - 1)]);
        itemsForSale.Add(unlockedAbilities[Random.Range(0, unlockedAbilities.Count - 1)]);

        //Debug.Log(itemsForSale[0].itemName + itemsForSale[1].itemName + itemsForSale[2].itemName + itemsForSale[3].itemName + itemsForSale[4].itemName);

        // have consumables for sale diferent
        while (itemsForSale[3].itemName == itemsForSale[4].itemName)
        {
            itemsForSale[4] = unlockedAbilities[Random.Range(0, unlockedAbilities.Count - 1)];
        }

        // spawn itemsForSale (child of shopkeeper ?)
        Vector3 offset = Vector3.zero;
        foreach (Item i in itemsForSale)
        {
            ItemManager.SpawnItem(i.itemName, gameObject.transform.position + offset);
            offset.z += 1;
        }
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
