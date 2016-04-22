using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
//Setup of Basic ItemManager Class for keeping track of items in game
*/

public static class ItemManager
{
    private static Dictionary<string, GameObject> ItemDictionary;

    static ItemManager()
    {
        ItemDictionary = new Dictionary<string, GameObject>();
        LoadItems();
    }

    //Used for the initial load of the ItemDictionary at runtime
    private static void LoadItems()
    {
        GameObject[] items = Resources.LoadAll("Items").Select(o => o as GameObject).ToArray();
        foreach (var item in items)
        {
            ItemDictionary.Add(item.name, item);
            Debug.Log(item.name);
        }
    }

    //Used to spawn an item based upon its name at a specific position
    //ToDo Possibly return the instantiated item to the user
    public static void SpawnItem(string itemKey, Vector3 spawnPosition)
    {
        if (itemKey != null)
        {
            GameObject.Instantiate(GetItem(itemKey), spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Spawning of an item failed!");
        }
    }

    public static void DestroyItem(GameObject itemObject)
    {
        GameObject.Destroy(itemObject);
    }

    public static GameObject GetItem(string key)
    {
        return ItemDictionary[key];
    }


}