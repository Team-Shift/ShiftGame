using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour {

	public Canvas can;

	void Start()
	{
		can.enabled = false;
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
			can.enabled = true;
            //SceneManager.LoadScene("Scarecrow_BossRoom");
            SceneManager.LoadScene("Alpha_Static");
        }
		else if(col.tag == "NPC")
		{
			Destroy (col.gameObject);
		}
    }

}
