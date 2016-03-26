using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class Room : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Hallways = new GameObject[4];
        //AssignHallways();
        //neighbors = new Dictionary<Direction, MapGenerator.Coord>();
    }

    public void Init()
    {
        Hallways = new GameObject[4];
        AssignHallways();
        neighbors = new Dictionary<Direction, Transform>();
        

        //neighbors[Direction.South].get
    }

	// Update is called once per frame
	void Update () {
	
	}

    public MapGenerator.Coord roomCoord;
    
    public Dictionary<Direction, Transform> neighbors;

    public GameObject[] Hallways;

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    private Direction swapDir(Direction location)
    {
        Direction retval = location;
            switch (location)
            {
                case Direction.North: retval = Direction.South; break;
                case Direction.East : retval = Direction.West;  break;
                case Direction.South: retval = Direction.North; break;
                case Direction.West : retval = Direction.East;  break;
            }

        return retval;
    }

    //Functions for adding and removing connected rooms
    public void AddNeighbor(Direction location, MapGenerator.Coord neighbor)
    {
        //neighbors.Add(location, gameObject.GetComponentInParent<MapGenerator>().roomLayout[neighbor.x, neighbor.y]);

        Transform t = gameObject.GetComponentInParent<MapGenerator>().roomLayout[neighbor.x, neighbor.y];

        // Little bit of danger here!    
        Transform portal = t.GetChild((int) swapDir(location) + 1).GetComponentInChildren<Portal>().transform; // for the active child           

        neighbors.Add(location, portal);
    }

    public void RemoveNeighbor(Direction location)
    {
        neighbors.Remove(location);
    }

    //GetSet Neighbor Active
    public void SetHallwayActive(Direction location, bool isActive)
    {
        switch (location)
        {
            case Direction.North:
                ActivateHallway(location, isActive);
                break;
            case Direction.East:
                ActivateHallway(location, isActive);
                break;
            case Direction.South:
                ActivateHallway(location, isActive);
                break;
            case Direction.West:
                ActivateHallway(location, isActive);
                break;
        }
    }

    private void AssignHallways()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child != null) { 
                if (child.gameObject.name == "North_Hallway")
                {
                    Hallways[(int)Direction.North] = child.gameObject;
                }
                if (child.gameObject.name == "East_Hallway")
                {
                    Hallways[(int)Direction.East] = child.gameObject;
                }
                if (child.gameObject.name == "South_Hallway")
                {
                    Hallways[(int)Direction.South] = child.gameObject;
                }
                if (child.gameObject.name == "West_Hallway")
                {
                    Hallways[(int)Direction.West] = child.gameObject;
                }
            }
        }
    }

    private void ActivateHallway(Direction location, bool isActive)
    {
        for (int i = 0; i < Hallways[(int)location].transform.childCount; i++)
        {
            Transform child = Hallways[(int)location].transform.GetChild(i);
            if (child.gameObject.name == "Active")
            {
                child.gameObject.SetActive(isActive);
            }
            if (child.gameObject.name == "Inactive")
            {
                child.gameObject.SetActive(!isActive);
            }
        }
    }

    //OnEntrance of a room
    //If First time entered
}
