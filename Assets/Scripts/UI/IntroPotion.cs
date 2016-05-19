using UnityEngine;
using System.Collections;

public class IntroPotion : MonoBehaviour {
    IntroManager intMan;
    public GameObject manager;
    public int index;
	// Use this for initialization
	void Start () {
        intMan = manager.GetComponent<IntroManager>();
	}
	
	void OnTriggerEnter()
    {
        intMan.AddItemToInv(ItemManager.GetItem("Healing Potion"), index);
        gameObject.active = false;
    }
}
