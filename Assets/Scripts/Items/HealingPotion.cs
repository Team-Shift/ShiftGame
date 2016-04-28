using UnityEngine;
using System;
using UnityEngine.Networking;

public class HealingPotion : Item, iConsumable
{
    public void OnUse(GameObject player)
    {
        //Be Used by someone
        //player.GetComponent<PlayerCombat>().addHealth(1);

        //Heal Someone
        Debug.Log("Attempting to use item");
    }
}