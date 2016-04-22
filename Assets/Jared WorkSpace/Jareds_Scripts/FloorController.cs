using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorController : MonoBehaviour
{
    public float groundLevel = 0;
    private List<GameObject> floorPieces = new List<GameObject>();
    
    private List<float> floorPieceLevels = new List<float>();
    public Custom2DController player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Custom2DController>();
        floorPieces.AddRange(GameObject.FindGameObjectsWithTag("PuzzleFloor"));
        foreach(GameObject piece in floorPieces)
        {
            floorPieceLevels.Add(piece.transform.position.y);
        }

        Debug.Log(floorPieces.Count);
        Debug.Log(floorPieceLevels.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.CameraSwitch == false)
        {
            for (int i = 0; i < floorPieces.Count; i++)
            {
                if(floorPieces[i].transform.position.y > groundLevel)
                {
                    Debug.Log("Moving Pieces down");
                    floorPieces[i].transform.position = new Vector3(floorPieces[i].transform.position.x, groundLevel, floorPieces[i].transform.position.z);
                }
            }
        }

        else if (player.CameraSwitch == true)
        {
            for (int i = 0; i < floorPieces.Count; i++)
            {
                if (floorPieces[i].transform.position.y < floorPieceLevels[i])
                {
                    Debug.Log("Moving pieces up");
                    floorPieces[i].transform.position = new Vector3(floorPieces[i].transform.position.x, floorPieceLevels[i], floorPieces[i].transform.position.z);
                }
            }
        }
    }
}
