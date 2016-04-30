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

    static ItemManager()
    {
        ItemDictionary = new Dictionary<string, Item>();
        UnlockedItems = new Dictionary<string, Item>();
        LoadItems();
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

    //Used to spawn an item based upon its name at a specific position
    //ToDo Possibly return the instantiated item to the user
    public static GameObject SpawnItem(string itemKey, Vector3 spawnPosition)
    {
        if (itemKey != null)
        {
            Debug.Log(GetItem(itemKey));
            GameObject g =  GameObject.Instantiate(GetItem(itemKey).gameObject, spawnPosition, Quaternion.identity) as GameObject;
            return g;
        }
        else
        {
            Debug.LogError("Spawning of an item failed!");
            return null;
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

    public static void UnlockItem(Item unlocked_item)
    {
        UnlockedItems.Add(ItemDictionary[unlocked_item.name].name, unlocked_item);
        Debug.Log(UnlockedItems.Count);
    }

    //Returns a list of weapons from the UnlockedItems List;
    //Parameter-Item.ItemType: Type of unlocked items you want returned
    public static List<Item> GetUnlockedItems(Item.ItemType item_type)
    {
        List<Item> unlockedItems = new List<Item>();

        foreach (var item in UnlockedItems)
        {
            if (item.Value.itype == item_type)
            {
                unlockedItems.Add(item.Value);
            }
        }
        return unlockedItems;
    }
}