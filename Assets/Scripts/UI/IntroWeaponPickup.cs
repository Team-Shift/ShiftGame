using UnityEngine;
using System.Collections;

public class IntroWeaponPickup : MonoBehaviour {
    public GameObject weapon;
    private IntroManager intMan;
    public GameObject manager;
	// Use this for initialization
	void Start () {
        intMan = manager.GetComponent<IntroManager>();
    }
	
	void OnTriggerEnter()
    {
        weapon.active = true;
        intMan.EnableHUD();
        intMan.AddItemToInv(ItemManager.GetItem("WoodSword"), 0);
        gameObject.active = false;
    }
}
