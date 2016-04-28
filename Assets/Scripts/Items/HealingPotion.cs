using UnityEngine;
using System;

public class HealingPotion : Item, iConsumable 
{
    // Use this for initialization
    void Start ()
	{

	}

	// Update is called once per frame
    
    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        Custom2DController playerController = col.gameObject.GetComponent<Custom2DController>();
    //        Debug.Log(col.name);
    //        if (playerController == null)
    //        {
    //            Debug.LogError("No Player Controller Found");
    //        }

    //        else
    //        {
    //            ItemManager.DestroyItem(gameObject);
    //            Debug.Log("Being picked up by" + playerController.gameObject.name);
    //        }
    //    }
    //}

    public void OnUse(GameObject player)
    {
        //Be Used by someone
        //player.GetComponent<PlayerCombat>().addHealth(1);

        //Heal Someone
        Debug.Log("Attempting to use item");
    }
}