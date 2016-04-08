using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class PlayerWorldManager_Temp : MonoBehaviour
{

    MenuManager sceneShit;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        sceneShit = FindObjectOfType<MenuManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= 0)
        {
            sceneShit.TownScene();
        }

        if(player.GetComponent<Custom2DController>().Health <= 0)
        {
            sceneShit.TownScene();
        }
    }
}
