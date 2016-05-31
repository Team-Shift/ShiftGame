using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class Room{

    public Room(GameObject roomPrefab)
    {
        hallways = new Dictionary<Direction, GameObject>();
        neighbors = new Dictionary<Direction, Room>();
        prefab = roomPrefab;
        
    }

    public void Init()
    {
        AssignHallways();
        SetHallwaysActive(true);
    }

    //Used to keep track of original prefab
    public GameObject prefab;
    
    //Prefab Instance - Instantiated Object
    public GameObject roomInst;

    public Vector3 roomPosition;

    public Dictionary<Direction, GameObject> hallways;

    public Dictionary<Direction, Room> neighbors;

    public bool isSeeded;

    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
        COUNT,
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
    public void AddNeighbor(Direction location, Room neighbor)
    {
        //Transform t = neighbor.prefab.transform;

        // Little bit of danger here!    
        //Transform portal = t.GetChild((int)swapDir(location) + 1).GetComponentInChildren<Portal>().transform; // for the active child           

        neighbors.Add(location, neighbor);

        
    }

    public void RemoveNeighbor(Direction location)
    {
        neighbors.Remove(location);
    }

    //GetSet Neighbor Active
    public void SetHallwaysActive(bool isActive)
    {
        foreach (KeyValuePair<Direction, Room> pair in neighbors)
        {
            ActivateHallway(pair.Key, isActive);
        }
    }

    //Assigns hallways for the room instance
    //Requires a room instance
    private void AssignHallways()
    {
        for (int i = 0; i < roomInst.transform.childCount; i++)
        {
            Transform child = roomInst.transform.GetChild(i);
            if(child != null) { 
                if (child.gameObject.name == "North_Hallway")
                {
                    hallways[Direction.North] = child.gameObject;
                }
                if (child.gameObject.name == "East_Hallway")
                {
                    hallways[Direction.East] = child.gameObject;
                }
                if (child.gameObject.name == "South_Hallway")
                {
                    hallways[Direction.South] = child.gameObject;
                }
                if (child.gameObject.name == "West_Hallway")
                {
                    hallways[Direction.West] = child.gameObject;
                }
            }
        }
    }

    private void ActivateHallway(Direction location, bool isActive)
    {
        for (int i = 0; i < hallways[location].transform.childCount; i++)
        {
            Transform child = hallways[location].transform.GetChild(i);
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

    public void SetPortalTarget()
    {
        foreach (KeyValuePair<Direction, Room> pair in neighbors)
        {
            Portal myPortal = hallways[pair.Key].GetComponentInChildren<Portal>();

            if (myPortal != null)
            {
                myPortal.name = (pair.Key + "Portal");
                Room neighbor = pair.Value;

                Vector3 targetOffset = new Vector3(0,1.0f,0);

                switch (pair.Key)
                {
                    case Direction.North:
                        targetOffset += new Vector3(0,0,1.5f);
                        myPortal.targetDirection = Direction.North;
                        break;
                    case Direction.East:
                        targetOffset += new Vector3(1.5f, 0, 0);
                        myPortal.targetDirection = Direction.East;
                        break;
                    case Direction.South:
                        targetOffset += new Vector3(0, 0, -1.5f);
                        myPortal.targetDirection = Direction.South;
                        break;
                    case Direction.West:
                        targetOffset += new Vector3(-1.5f, 0, 0);
                        myPortal.targetDirection = Direction.West;
                        break;
                }

                Portal neighborPortal = neighbor.hallways[swapDir(pair.Key)].GetComponentInChildren<Portal>();

                if (neighborPortal != null)
                {
                    targetOffset += new Vector3(neighborPortal.transform.position.x, 0, neighborPortal.transform.position.z);
                    myPortal.targetPosition += targetOffset;
                }
                else
                {
                    Debug.LogError("No Neighbor Portal Found on room:" + neighbor.prefab.name + "!");
                }
            }
        }
    }

    //OnEntrance of a room
    //If First time entered
}
