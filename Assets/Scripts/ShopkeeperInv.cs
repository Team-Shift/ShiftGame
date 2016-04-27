using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopkeeperInv : MonoBehaviour {

    // { [0]Weapon, [1]armor, [2]consumable, [3]consumable, [4]ability }
    public ItemLibrary.ItemData[]itemsForSale;
    public int numItems = 5;

    public float resellPerc = 0.3f;

    // Use this for initialization
    void Start () {
        // create array
        itemsForSale = new ItemLibrary.ItemData[numItems];

        // populate list dependent on unlocked items
        GenerateInventory();
	}

    // call when player completes dungeon, dies, or teleports out
    void GenerateInventory()
    {
        ItemLibrary itemDB = new ItemLibrary();

        List<ItemLibrary.ItemData> unlockedWeapons = new List<ItemLibrary.ItemData>();
        List<ItemLibrary.ItemData> unlockedArmor = new List<ItemLibrary.ItemData>();
        List<ItemLibrary.ItemData> unlockedConsumables = new List<ItemLibrary.ItemData>();
        List<ItemLibrary.ItemData> unlockedAbilities = new List<ItemLibrary.ItemData>();

        // seperate unlocked items
        foreach (ItemLibrary.ItemData item in itemDB.allItems)
        {
            if(item.unlocked)
            {
                if(item.itemType == Item.ItemType.WEAPON)
                    unlockedWeapons.Add(item);
                else if(item.itemType == Item.ItemType.ARMOR)
                    unlockedArmor.Add(item);
                else if (item.itemType == Item.ItemType.CONSUMABLE)
                    unlockedConsumables.Add(item);
                else if (item.itemType == Item.ItemType.ABILITY)
                    unlockedAbilities.Add(item);
            }
        }

        // randomly choose items
        itemsForSale[0] = unlockedWeapons[Random.Range(0, unlockedWeapons.Count-1)];
        itemsForSale[1] = unlockedArmor[Random.Range(0, unlockedArmor.Count - 1)];
        itemsForSale[2] = unlockedConsumables[Random.Range(0, unlockedWeapons.Count - 1)];
        itemsForSale[3] = unlockedConsumables[Random.Range(0, unlockedWeapons.Count - 1)];
        itemsForSale[4] = unlockedAbilities[Random.Range(0, unlockedAbilities.Count - 1)];
        // have consumables for sale diferent
        while(itemsForSale[3].ID == itemsForSale[4].ID)
        {
            itemsForSale[4] = unlockedAbilities[Random.Range(0, unlockedAbilities.Count - 1)];
        }
    }

    void PlayerBuyItem(int index)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Inventory inv = player.GetComponent<Inventory>();
        // decrement gold
        inv.goldCount -= itemsForSale[index].value;
        // change display
        player.GetComponent<InvHUD>().ReduceGold(itemsForSale[index].value);

        // if slot available: add to inventory
        //inv.invItems[]

        // else replace w/current and add current to bank
    }

    int PlayerSellItem(int id)
    {
        ItemLibrary itemDB = new ItemLibrary();

        int sellingPrice = (int)((float)itemDB.allItems[id].value * resellPerc);
        Debug.Log("sell for " + sellingPrice + "?");
        return sellingPrice;
    }
}
