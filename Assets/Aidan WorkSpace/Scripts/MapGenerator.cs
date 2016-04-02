using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    //ToDo Remove array of maps/dungeons
    //Only need the single dungeon per generation
    //Unless we want to do something like reset/shuffle dungeon while inside?
    //ToDo Rename from MapGen to DungeonGen
    public Dungeon[] dungeons;
    public int dungeonIndex;

    public GameObject[] rooms;

    //For Debug Purposes
    public GameObject roomPrefab;

    public float gridScale;

    List<Coord> allRoomCoords;
    Queue<Coord> shuffleRoomCoords;
    Queue<Coord> shuffleOpenRoomCoords;
    public List<Coord> currentOpenCoords;

    //Transform[,] roomMap;
    public Transform[,] roomLayout;
    Dungeon currentDungeon;
    


    //
    public Room[,] map;

    //Room definitions
    public GameObject StartRoom;
    public GameObject EndRoom;

    //
    private List<Room> PossibleBossRooms;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        //Grabs the initial map from the array
        currentDungeon = dungeons[dungeonIndex];

        roomLayout = new Transform[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];
        map = new Room[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];
        PossibleBossRooms = new List<Room>();

        System.Random prng = new System.Random(currentDungeon.seed);

        //Generate array of room prefabs
        rooms = Resources.LoadAll("Rooms", typeof(Transform)).Select( o => o as GameObject ).ToArray();

        //Create array of rooms and assign room types
        //ToDo If x,y = center assign the start room
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                Room newRoom;
                if (x == currentDungeon.dungeonCenter.x && y == currentDungeon.dungeonCenter.y)
                {
                    newRoom = new Room(StartRoom);
                }
                else
                {
                    newRoom = new Room(roomPrefab);
                }
                
                newRoom.roomPosition = CoordToPosition(x, y);
                map[x, y] = newRoom;
            }
        }

        //Generates a list of possible room coordinates
        //ToDo Needed for now remove as soon as possible
        allRoomCoords = new List<Coord>();
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                allRoomCoords.Add(new Coord(x, y));
            }
        }

        shuffleRoomCoords = new Queue<Coord>(Utility.ShuffleArray(allRoomCoords.ToArray(), currentDungeon.seed));

        //Create the parent object for the Dungeon
        string holderName = "Generated Dungeon";
        if (transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }

        Transform dungeonHolder = new GameObject(holderName).transform;
        dungeonHolder.SetParent(transform, false);

#region RandomRemoveRoom
        //Create an array of removable coordinates for rooms
        bool[,] obstacleMap = new bool[(int)currentDungeon.dungeonSize.x, (int)currentDungeon.dungeonSize.y];

        int obstacleCount = (int)(currentDungeon.dungeonSize.x * currentDungeon.dungeonSize.y * currentDungeon.emptyRoomPercent);
        int currentObstacleCount = 0;
        currentOpenCoords = new List<Coord>(allRoomCoords);

        //Get a Random Coord
        //If the coord is not the center room
        //Remove the Room from that coord
        //Look here for dead stuff
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != currentDungeon.dungeonCenter && MapIsAccessible(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToPosition(randomCoord);

                map[randomCoord.x, randomCoord.y] = null;
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }

        }
        #endregion

#region RoomGeneration
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                Room newRoom = map[x, y];

                if (newRoom != null)
                {
                    //ToDo Figure out better way that keeping track of a prefab and a gameobject
                    //Can I create the new GameObject without instantiated it and do that later?
                    GameObject newRoomObject = Instantiate(newRoom.prefab);
                    newRoom.roomInst = newRoomObject;
                    newRoom.Init();
                    newRoomObject.name = ("RoomPosition(" + x + "," + y + ")");
                    
                    newRoomObject.transform.SetParent(dungeonHolder, false);
                    newRoomObject.transform.localPosition = newRoom.roomPosition;
                    newRoomObject.transform.localRotation = Quaternion.identity;
                    newRoomObject.transform.localScale = Vector3.one;


                    for (int row = -1; row <= 1; row++)
                    {
                        for (int col = -1; col <= 1; col++)
                        {
                            int neighborX = x + row;
                            int neighborY = y + col;
                            if (row == 0 || col == 0)
                            {
                                if (neighborX >= 0 && neighborX < currentDungeon.dungeonSize.x && neighborY >= 0 && neighborY < currentDungeon.dungeonSize.y)
                                {
                                    Room neighbor = map[neighborX, neighborY];
                                    if (neighbor != null)
                                    {
                                        if (neighborY > y)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.North, neighbor);
                                            newRoom.SetHallwayActive(Room.Direction.North, true);
                                            newRoom.SetPortalTarget(Room.Direction.North, neighbor.roomPosition);
                                        }
                                        if (neighborX > x)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.East, neighbor);
                                            newRoom.SetHallwayActive(Room.Direction.East, true);
                                            newRoom.SetPortalTarget(Room.Direction.East, neighbor.roomPosition);
                                        }
                                        if (neighborY < y)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.South, neighbor);
                                            newRoom.SetHallwayActive(Room.Direction.South, true);
                                            newRoom.SetPortalTarget(Room.Direction.South, neighbor.roomPosition);
                                        }
                                        if (neighborX < x)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.West, neighbor);
                                            newRoom.SetHallwayActive(Room.Direction.West, true);
                                            newRoom.SetPortalTarget(Room.Direction.West, neighbor.roomPosition);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Adding Neighbors Completed
                    Debug.Log(newRoom.roomInst.name +" neighbor count: " + newRoom.neighbors.Count);
                    
                    //Define Possible Boss Rooms
                    //If there are no rooms with only one neighbor we look for rooms with 2
                    //Etc?
                    if (newRoom.prefab != StartRoom)
                    {
                        if (newRoom.neighbors.Count == 1)
                        {
                            
                            PossibleBossRooms.Add(newRoom);
                        }
                    }
                }
            }
        }
        #endregion
        //Select Random Room from BoosRooms
        //ToDo See Fix to shitty instantiation above
        Debug.Log(PossibleBossRooms.Count);
        int randomIndex = prng.Next(0, PossibleBossRooms.Count);
        //Room bossRoom = PossibleBossRooms[UnityEngine.Random.Range(0, PossibleBossRooms.Count - 1)];
        Room bossRoom = PossibleBossRooms[randomIndex];
        bossRoom.roomInst = Instantiate(EndRoom);
        bossRoom.roomInst.name= ("BossRoom");

        bossRoom.roomInst.transform.SetParent(dungeonHolder, false);
        bossRoom.roomInst.transform.localPosition = bossRoom.roomPosition;
        bossRoom.roomInst.transform.localRotation = Quaternion.identity;
        bossRoom.roomInst.transform.localScale = Vector3.one;

        //DestroyImmediate(bossRoom.roomInst);
    }

    //A* based check to ensure that the removable rooms allow remaining rooms to be accessed
    bool MapIsAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        //Flood Fill to check for accessible counts
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentDungeon.dungeonCenter);

        mapFlags[currentDungeon.dungeonCenter.x, currentDungeon.dungeonCenter.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighborX = tile.x + x;
                    int neighborY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighborX >= 0 && neighborX < obstacleMap.GetLength(0) && neighborY >= 0 && neighborY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighborX, neighborY] && !obstacleMap[neighborX, neighborY])
                            {
                                mapFlags[neighborX, neighborY] = true;
                                queue.Enqueue(new Coord(neighborX, neighborY));

                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(currentDungeon.dungeonSize.x * currentDungeon.dungeonSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;

    }

    //Converts a grid/array coordinate to a position in space
    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-currentDungeon.dungeonSize.x / 2f + 0.5f + (x * gridScale) - gridScale, 0, -currentDungeon.dungeonSize.y / 2f + 0.5f + (y * gridScale) - gridScale);
    }

    Vector3 CoordToPosition(Coord coordinate)
    {
        return new Vector3(-currentDungeon.dungeonSize.x / 2f + 0.5f + (coordinate.x * gridScale) - gridScale, 0, -currentDungeon.dungeonSize.y / 2f + 0.5f + (coordinate.y * gridScale) - gridScale);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffleRoomCoords.Dequeue();
        shuffleRoomCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Transform GetRandomOpenRoom()
    {
        Coord randomCoord = shuffleOpenRoomCoords.Dequeue();
        shuffleOpenRoomCoords.Enqueue(randomCoord);

        return roomLayout[randomCoord.x, randomCoord.y];
    }

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
    }

    [System.Serializable]
    public class Dungeon
    {
        public Coord dungeonSize;
        [Range(0, 1)]
        public float emptyRoomPercent;

        public int seed;

        public Coord dungeonCenter
        {
            get
            {
                return new Coord(dungeonSize.x / 2, dungeonSize.y / 2);
            }
        }
    }

    //public class Room
    //{
    //    public Coord roomCoord;
    //    //public List<Coord> neighbors;

    //    public Dictionary<Direction, Coord> neighbors; 

    //    public Transform roomPrefab;

    //    public enum Direction
    //    {
    //        North,
    //        East,
    //        South,
    //        West
    //    }

    //    public Direction TheNeighbors;

    //    public Room(Coord coord, Transform prefab)
    //    {
    //        roomCoord = coord;
    //        roomPrefab = prefab;
    //        neighbors = new Dictionary<Direction, Coord>();
    //    }

    //    //Functions for adding and removing connected rooms
    //    public void AddNeighbor(Direction location,Coord neighbor)
    //    {
    //        neighbors.Add(location, neighbor);
    //    }
    //    public void RemoveNeighbor(Direction location)
    //    {
    //        neighbors.Remove(location);
    //    }
        
    //    //OnEntrance of a room
    //    //If First time entered
    //}
}
