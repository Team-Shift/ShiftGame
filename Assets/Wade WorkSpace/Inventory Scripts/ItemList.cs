using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// stored data of all possible items
public class ItemList : MonoBehaviour {
    // item ID = index
    public struct ItemData
    {
        public Sprite sprite;
        public GameObject mesh;
        public bool unlocked;
        // Action (what the item does)
    }

    ItemData[] allItems;
    int numItems = 10;

    void Start()
    {
        allItems = new ItemData[numItems];

        unlockItem(0);
    }

    void unlockItem(int itemID)
    {
        allItems[itemID].unlocked = true;
    }
}
