using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostCircleControl : MonoBehaviour
{
    List<Transform> ghosts;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ghosts = new List<Transform>();

        foreach(Transform child in gameObject.transform)
        {
            ghosts.Add(child);
        }
        Debug.Log(ghosts.Count);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 1);

        foreach(Transform ghost in ghosts)
        {
            if(!ghost)
            {
                Destroy(gameObject);
            }
            if (ghost)
            {
                ghost.LookAt(player.transform.position);
            }
        }
    }
}
