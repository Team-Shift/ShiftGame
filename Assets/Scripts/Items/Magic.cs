using UnityEngine;
using System.Collections;

public class Magic : Item, iConsumable
{
    public GameObject projectile;
    public void OnUse(GameObject g)
    {
        //g.GetComponent<Custom2DController>().playerDir;
        Quaternion rot = gameObject.transform.rotation;
        //rot *= Quaternion.Euler(0, rotOffset, 0); // rotating wierdly 
        // instantiate fire
        Instantiate(projectile, transform.position, rot);
    }
}
