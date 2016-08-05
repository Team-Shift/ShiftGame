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
	
	void OnTriggerEnter(Collider other)
    {
	    if (other.tag == "Player")
	    {
	        Weapon playerWeapon = other.gameObject.GetComponentInChildren<Weapon>(true);
            playerWeapon.gameObject.SetActive(true);
			intMan.EnableHUD();
			intMan.AddItemToInv(ItemManager.GetItem("WoodSword"), 0);
			gameObject.SetActive(false);
	    }
        
    }
}
