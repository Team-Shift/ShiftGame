using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
//Setup of Basic ItemManager Class for keeping track of items in game
*/

public static class ItemManager
{
    private static Dictionary<string, Item> ItemDictionary;
    private static Dictionary<string, Item> UnlockedItems;

    // ToDo Decide if this is where we want BankedItems stored
    // Banked items with a set/get instead of leaving public
    public static Dictionary<string, Item> BankedItems;

    static ItemManager()
    {
        ItemDictionary = new Dictionary<string, Item>();
        UnlockedItems = new Dictionary<string, Item>();
        BankedItems = new Dictionary<string, Item>();
        LoadItems();
        //********************* FOR TESTING ****************//
        //UnlockAllItems();
        UnlockItem("Bow");
        UnlockItem("Axe");
        UnlockItem("Healing Potion");
        UnlockItem("WoodSword");
        UnlockItem("Sword");
        //**************************************************//
    }

    //Used for the initial load of the ItemDictionary at runtime
    private static void LoadItems()
    {
        GameObject[] items = Resources.LoadAll("Items").Select(o => o as GameObject).ToArray();
        foreach (var item in items)
        {
            ItemDictionary.Add(item.name, item.GetComponent<Item>());
            //Debug.Log(item.name);
        }
    }

    // Used to spawn an item based upon its name at a specific position
    // Returns the item that was spawned
    public static GameObject SpawnItem(string itemKey, Vector3 spawnPosition)
    {
        if (itemKey != null)
        {
            //Debug.Log(GetItem(itemKey));
            return  GameObject.Instantiate(GetItem(itemKey).gameObject, spawnPosition, Quaternion.identity) as GameObject;
            //return g;
        }
        else
        {
            Debug.LogError("Spawning of an item failed!");
            return null;
        }
    }

    public static GameObject SpawnItem(string itemKey)
    {
        return SpawnItem(itemKey, Vector3.zero);
    }

    //Add item to player bank
    public static void BankItem(string itemKey)
    {
        if (itemKey != null)
        {
            BankedItems.Add(itemKey, ItemDictionary[itemKey]);
        }
    }

    public static void DestroyItem(GameObject itemObject)
    {
        GameObject.Destroy(itemObject);
    }

    public static Item GetItem(string key)
    {
        return ItemDictionary[key];
    }

    public static void UnlockItem(string itemName)
    {
        UnlockedItems.Add(itemName, ItemDictionary[itemName]);
        // only add if item doesnt exist
        if (ItemDictionary[itemName] == null)
        {
            
        }
    }

    //Returns a list of weapons from the UnlockedItems List;
    //Parameter-Item.ItemType: Type of unlocked items you want returned
    public static List<Item> GetUnlockedItems(Item.ItemType item_type)
    {
        List<Item> tempItems = new List<Item>();

        foreach (var item in UnlockedItems)
        {
            //Debug.Log(item);
            if(item.Value != null)
            {
                if (item.Value.itype == item_type)
                {
                    tempItems.Add(item.Value);
                }
            }
        }
        //Debug.Log(tempItems.Count);
        return tempItems;
    }


    // For Debug Purposes
    // unlocks all items in Resources folder **only for testing purposes**
    public static List<Item> GetUnlockedItems()
    {
        return UnlockedItems.Values.ToList();
    }

    // For Debug Purposes
    // unlocks all items in Resources folder **only for testing purposes**
    public static void UnlockAllItems()
    {
        foreach(var v in ItemDictionary)
        {
            if (!UnlockedItems.Contains(v))
            {
                UnlockedItems.Add(v.Key, v.Value);
            }
        }
    }

    // For Debug Purposes
    // Banks some items initially
    public static void BankItemSet(Item.ItemType item_Type)
    {
        foreach (var item in ItemDictionary)
        {
            if (item.Value != null)
            {
                if (item.Value.itype == item_Type)
                {
                    BankItem(item.Key);
                }
            }
        }
        Debug.Log(BankedItems.Count);
    }

    public static void ClearBank()
    {
        BankedItems.Clear();
    }
}