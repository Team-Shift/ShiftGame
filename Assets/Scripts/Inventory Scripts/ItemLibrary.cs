using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
// stored data of all possible items
public class ItemLibrary : MonoBehaviour {
    // item ID = index
    public struct ItemData
    {
        public int ID;
        public GameObject mesh;
        public Sprite sprite;
        public Item.ItemType itemType;
        public bool unlocked;
        public string name;
        public int value;
    }

    public ItemData[] allItems;
    int numItems = 10;

    void Start()
    {
        allItems = new ItemData[numItems];

        unlockItem(0);  //potion
        unlockItem(1);  //sword
    }

    // for library of all discoverable items
    void unlockItem(int itemID)
    {
        allItems[itemID].unlocked = true;
    }
}
