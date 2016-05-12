using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    public Dungeon[] dungeons;
    public int dungeonIndex;

    //For Debug Purposes
    public GameObject roomPrefab;

    public bool isSeeded;

    public float gridScale;

    List<Coord> allRoomCoords;
    Queue<Coord> shuffleRoomCoords;
    Queue<Coord> shuffleOpenRoomCoords;

    private List<Coord> currentOpenCoords;

    //Transform[,] roomMap;
    public Transform[,] roomLayout;
    Dungeon currentDungeon;
    
    //
    public Room[,] map;

    //Room definitions
    public GameObject StartRoom;
    public GameObject EndRoom;

    //
    private List<Coord> PossibleBossRooms;

    void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        //Grabs the initial map from the array
        currentDungeon = dungeons[dungeonIndex];

        roomLayout = new Transform[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];
        map = new Room[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];
        PossibleBossRooms = new List<Coord>();

        System.Random prng = new System.Random(currentDungeon.seed);

        //Generate array of room prefabs
        GameObject[] rooms = Resources.LoadAll("Rooms").Select(o => o as GameObject).ToArray();

        //Create array of rooms and assign room types
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

        //Check if dungeon is intended to be seeded or not
        if (isSeeded)
        {
            shuffleRoomCoords = new Queue<Coord>(Utility.ShuffleArray(allRoomCoords.ToArray(), currentDungeon.seed));
        }
        else
        {
            shuffleRoomCoords = new Queue<Coord>(Utility.ShuffleArray(allRoomCoords.ToArray(), (int)System.DateTime.Now.Ticks));
        }

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

#region AssignNeighbors
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                Room newRoom = map[x, y];

                if (newRoom != null)
                {
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
                                        }
                                        if (neighborX > x)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.East, neighbor);
                                        }
                                        if (neighborY < y)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.South, neighbor);
                                        }
                                        if (neighborX < x)
                                        {
                                            newRoom.AddNeighbor(Room.Direction.West, neighbor);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Adding Neighbors Completed
                    map[x, y] = newRoom;
                }
            }
        }

        #endregion

        #region Filter Rooms
        //Use region to filter through values prior to assigning new prefabs to rooms
        //for special use cases (i.e Boss Rooms, One Offs, Etc.)

        //Define Possible Boss Rooms
        //If there are no rooms with only one neighbor we look for rooms with 2
        //Etc?
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                Room newRoom = map[x, y];

                if (newRoom != null)
                {
                    if (newRoom.prefab != StartRoom)
                    {
                        if (newRoom.neighbors.Count == 1)
                        {
                            PossibleBossRooms.Add(new Coord(x,y));
                        }
                    }
                }
                map[x, y] = newRoom;
            }
        }

        //Check here for room load troubles
        //get random room from above list and assign new prefab
        int randomBossIndex = prng.Next(0, PossibleBossRooms.Count);
        map[PossibleBossRooms[randomBossIndex].x, PossibleBossRooms[randomBossIndex].y].prefab = EndRoom;

        #endregion

        #region InstantiateRooms
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                Room newRoom = map[x, y];

                if (newRoom != null)
                {
                    // Select random room prefab from array to assign
                    // Only if not in the center room
                    if (newRoom != map[currentDungeon.dungeonCenter.x, currentDungeon.dungeonCenter.y])
                    {
                        int randomIndex = prng.Next(0, rooms.Length);
                        newRoom.prefab = rooms[randomIndex];
                    }


                    //Instantiate and initialize room
                    GameObject newRoomObject = Instantiate(newRoom.prefab);
                    newRoom.roomInst = newRoomObject;
                    newRoom.Init();
                    newRoomObject.name = ("RoomPosition(" + x + "," + y + ")");

                    newRoomObject.transform.SetParent(dungeonHolder, false);
                    newRoomObject.transform.localPosition = newRoom.roomPosition;
                    newRoomObject.transform.localRotation = Quaternion.identity;
                    newRoomObject.transform.localScale = Vector3.one;
                }
                map[x, y] = newRoom;
            }
        }

        #endregion

#region AssignPortalTargets
        //Loop through rooms after instantiation to assign portal targets
        for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
        {
            for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
            {
                Room newRoom = map[x, y];

                if (newRoom != null)
                {
                    newRoom.SetPortalTarget();
                }
                map[x, y] = newRoom;
            }
        }
#endregion

        Resources.UnloadUnusedAssets();
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
}
