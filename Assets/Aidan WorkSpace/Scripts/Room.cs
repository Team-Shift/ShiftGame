using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        neighbors = new Dictionary<Direction, MapGenerator.Coord>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public MapGenerator.Coord roomCoord;
    
    public Dictionary<Direction, MapGenerator.Coord> neighbors;

    private GameObject[] Hallways;

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    //Functions for adding and removing connected rooms
    public void AddNeighbor(Direction location, MapGenerator.Coord neighbor)
    {
        neighbors.Add(location, neighbor);
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
            if (child.gameObject.name == "North_Hallway")
            {
                Hallways[(int)Direction.North] = child.gameObject;
                //Activate proper hall
            }
            if (child.gameObject.name == "East_Hallway")
            {
                Hallways[(int)Direction.East] = child.gameObject;
                //Activate proper hall
            }
            if (child.gameObject.name == "South_Hallway")
            {
                Hallways[(int)Direction.South] = child.gameObject;
                //Activate proper hall
            }
            if (child.gameObject.name == "West_Hallway")
            {
                Hallways[(int)Direction.West] = child.gameObject;
                //Activate proper hall
            }
        }
    }

    private void ActivateHallway(Direction location, bool active)
    {
        for (int i = 0; i < Hallways[(int)location].transform.childCount; i++)
        {
            Transform child = Hallways[(int)location].transform.GetChild(i);
            if (child.gameObject.name == "Active")
            {
                if (active)
                {
                    child.gameObject.SetActive(active);
                }
                else
                {
                    child.gameObject.SetActive(!active);
                }
                //Activate proper hall
            }
            if (child.gameObject.name == "Inactive")
            {
                if (active)
                {
                    child.gameObject.SetActive(!active);
                }
                else
                {
                    child.gameObject.SetActive(active);
                }
            }
        }
    }

    //OnEntrance of a room
    //If First time entered
}
