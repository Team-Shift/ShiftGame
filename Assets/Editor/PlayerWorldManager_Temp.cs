using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class PlayerWorldManager_Temp : MonoBehaviour
{

    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= 0)
        {
            this.transform.LoadScene(1);
        }

        if(player.GetComponent<PlayerCombat>().Health <= 0)
        {
            this.transform.LoadScene(2);
        }
    }
}
