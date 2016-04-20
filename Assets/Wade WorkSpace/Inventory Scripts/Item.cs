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

    void OnTriggerEnter()
    {
        canPickup = true;

        // to display text on screen 
        /*GameObject g = new GameObject();
        g.transform.localScale *= 0.5f;
        g.transform.position = gameObject.transform.position;
        g.AddComponent<GUIText>().text = "press 'x' to pickup";*/

    }

    void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.X) && canPickup)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Inventory i = player.GetComponent<Inventory>();
                i.AddItem(this as Item);
                if(canPickup) Destroy(gameObject);
            }
            else
                Debug.Log("player not found");
        }
    }

    void OnTriggerExit()
    {
        canPickup = false;
    }
}
