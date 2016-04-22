using UnityEngine;
using System.Collections;

public class HealingPotion : MonoBehaviour
{

    //private GameObject player;
    private HealthUI player;


	// Use this for initialization
	void Start () {
	     player = player.GetComponent<HealthUI>();
	}
	
	// Update is called once per frame

    void OnUse()
    {
        player.HealHeart();
    }
}