using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene("Scarecrow_BossRoom");
            SceneManager.LoadScene("Alpha_Static");
        }
		else if(col.tag == "NPC")
		{
			Destroy (col.gameObject);
		}
    }

}
