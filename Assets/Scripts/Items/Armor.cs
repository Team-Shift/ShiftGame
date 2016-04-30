using UnityEngine;
using System.Collections;

public class Armor : Item ,iConsumable {

    public int value;
    public void OnUse(GameObject g)
    {
        g.GetComponent<PlayerCombat>().ModifyHealth(value);
        Debug.Log("increasing health by " + value);
    }

}
