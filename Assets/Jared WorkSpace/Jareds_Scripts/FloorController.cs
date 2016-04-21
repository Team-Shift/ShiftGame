using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorController : MonoBehaviour
{
    public List<GameObject> floorPieces = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        floorPieces.AddRange(GameObject.FindGameObjectsWithTag("PuzzleFloor"));
        Debug.Log(floorPieces.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
