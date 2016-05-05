﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorController : MonoBehaviour
{
    public float groundLevel = 0;
    private float posOffset = 0;
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
                if (floorPieces[i].transform.position.y > groundLevel)
                {
                    posOffset = floorPieces[i].GetComponent<BoxCollider>().size.x;
                    Vector3 floorPiecePos = floorPieces[i].transform.position;
                    Vector3 playerPos = player.gameObject.transform.position;

                    floorPieces[i].transform.position = new Vector3(floorPieces[i].transform.position.x, groundLevel, floorPieces[i].transform.position.z);

                    if (Mathf.Abs(playerPos.x - floorPiecePos.x) < posOffset && Mathf.Abs(playerPos.z - floorPiecePos.z) < posOffset)
                    {
                        Debug.Log("Putting the player back onto the ground");
                        player.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, groundLevel + 1, player.gameObject.transform.position.z);
                    }
                }
            }
        }

        else if (player.CameraSwitch == true)
        {
            for (int i = 0; i < floorPieces.Count; i++)
            {
                if (floorPieces[i].transform.position.y < floorPieceLevels[i])
                {
                    posOffset = floorPieces[i].GetComponent<BoxCollider>().size.x / 2;
                    Vector3 floorPiecePos = floorPieces[i].transform.position;
                    Vector3 playerPos = player.gameObject.transform.position;

                    if(Mathf.Abs(playerPos.x - floorPiecePos.x) < posOffset && Mathf.Abs(playerPos.z - floorPiecePos.z) < posOffset && playerPos.y > floorPiecePos.y)
                    {
                        float playerSizeOffset = player.gameObject.GetComponent<BoxCollider>().size.y / 2;

                        float pushUp = floorPieceLevels[i] - player.gameObject.transform.position.y;

                        player.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + (pushUp + playerSizeOffset + 1), player.gameObject.transform.position.z);
                                            
                        
                    }

                    floorPieces[i].transform.position = new Vector3(floorPieces[i].transform.position.x, floorPieceLevels[i], floorPieces[i].transform.position.z);
                }
            }
        }
    }
}