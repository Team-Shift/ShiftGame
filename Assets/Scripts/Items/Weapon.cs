using UnityEngine;
using System.Collections;

public class Weapon : Item, iEquipable
{
    public int atkAmount;
    public void OnUse(GameObject g)
    {
        // teleport to town
        g.GetComponent<PlayerCombat>().ModifyAttack(atkAmount);
    }
}
