using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    //[HideInInspector]
    public int ID;

    void Start()
    {
        // each item has a different ID
        ID = 0;
    }

    void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Inventory i = player.GetComponent<Inventory>();
        i.AddItem(this);
    }
}
