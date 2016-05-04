using UnityEngine;
using System;
using UnityEngine.Networking;

public class HealingPotion : Item, iConsumable
{
    public int value;
    public void OnUse(GameObject player)
    {
        //Be Used by someone
        player.GetComponent<PlayerCombat>().ModifyHealth(value);

        //update UI
        player.GetComponent<HealthUI>().HealHeart();

        // delete item in inventory

        //Heal Someone
        //Debug.Log("healing by " + value);
    }
}