using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Scarecrow_BossRoom");
        }
    }
}
