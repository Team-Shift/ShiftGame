using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {

    public int ID;
    public string itemName;
    [HideInInspector]
    public bool canPickup;

    void Start()
    {
        canPickup = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") canPickup = true;
        if (canPickup)
        {
            //canPickup = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) //make sure player is found
            {
                Inventory i = player.GetComponent<Inventory>();
                i.AddItem(this);
                if (canPickup) Destroy(gameObject);
            }
            else
                Debug.Log("player not found");
        }

        // to display text on screen 
        /*GameObject g = new GameObject();
        g.transform.localScale *= 0.5f;
        g.transform.position = gameObject.transform.position;
        g.AddComponent<GUIText>().text = "press 'x' to pickup";*/

    }

    void OnTriggerStay()
    {
        
    }

    void OnTriggerExit()
    {
        canPickup = false;
    }
}
