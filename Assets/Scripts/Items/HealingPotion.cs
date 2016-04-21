using UnityEngine;
using System.Collections;

public class HealingPotion : MonoBehaviour
{

    //private GameObject player;
    private Custom2DController player;


	// Use this for initialization
	void Start () {
	     player = player.GetComponent<Custom2DController>();
	}
	
	// Update is called once per frame

    void OnUse()
    {
        player.HealHeart();
    }
}