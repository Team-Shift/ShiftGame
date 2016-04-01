using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class Room{

    public Room(GameObject roomPrefab)
    {
        Hallways = new GameObject[4];
        neighbors = new Dictionary<Direction, Transform>();
        prefab = roomPrefab;
        
    }

    public void Init()
    {
        AssignHallways();
    }

    //Used to keep track of original prefab
    public GameObject prefab;
    
    //Prefab Instance - Instantiated Object
    public GameObject roomInst;

    public Vector3 roomPosition;
    
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
    public void AddNeighbor(Direction location, Room neighbor)
    {
        //ToDo Fix Portal Bullshit and get stuff on screen
        Transform t = neighbor.prefab.transform;

        // Little bit of danger here!    
        //Transform portal = t.GetChild((int)swapDir(location) + 1).GetComponentInChildren<Portal>().transform; // for the active child           

        neighbors.Add(location, t);
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

    //Assigns hallways for the room instance
    private void AssignHallways()
    {
        for (int i = 0; i < roomInst.transform.childCount; i++)
        {
            Transform child = roomInst.transform.GetChild(i);
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

    public void SetPortalTarget(Direction location, Vector3 target)
    {
        Portal myNPortal = Hallways[(int)location].GetComponentInChildren<Portal>();
        myNPortal.name = (location + "Portal");
        myNPortal.targetPosition = target;
    }

    //OnEntrance of a room
    //If First time entered
}
