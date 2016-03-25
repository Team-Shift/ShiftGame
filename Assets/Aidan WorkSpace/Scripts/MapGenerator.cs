using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

public class MapGenerator : MonoBehaviour
{
    //ToDo Remove array of maps/dungeons
    //Only need the single dungeon per generation
    //Unless we want to do something like reset/shuffle dungeon while inside?
    //ToDo Rename from MapGen to DungeonGen
    public Dungeon[] dungeons;
    public int dungeonIndex;

    public Transform[] rooms;

    public Transform roomPrefab;

    public float gridScale;

    List<Coord> allRoomCoords;
    Queue<Coord> shuffleRoomCoords;
    Queue<Coord> shuffleOpenRoomCoords;

    //Transform[,] roomMap;
    Transform[,] roomLayout;
    Dungeon currentDungeon;

    void Start()
    {
        
    }

    public void GenerateMap()
    {
        //Grabs the initial map from the array
        currentDungeon = dungeons[dungeonIndex];

        roomLayout = new Transform[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];

        System.Random prng = new System.Random(currentDungeon.seed);

        //Generate array of room prefabs
        rooms = Resources.LoadAll("Rooms", typeof(Transform)).Select( o => o as Transform ).ToArray();

        //Generates a list of possible room coordinates
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

        //Create an array of removable coordinates for rooms
        bool[,] obstacleMap = new bool[(int)currentDungeon.dungeonSize.x, (int)currentDungeon.dungeonSize.y];

        int obstacleCount = (int)(currentDungeon.dungeonSize.x * currentDungeon.dungeonSize.y * currentDungeon.emptyRoomPercent);
        int currentObstacleCount = 0;
        List<Coord> currentOpenCoords = new List<Coord>(allRoomCoords);

        //Get a Random Coord
        //If the coord is not the center room
        //Remove the Room from that coord
        //
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != currentDungeon.dungeonCenter && MapIsAccessible(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToPosition(randomCoord);

                currentOpenCoords.Remove(randomCoord);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }

        }

        shuffleOpenRoomCoords = new Queue<Coord>(Utility.ShuffleArray(currentOpenCoords.ToArray(), currentDungeon.seed));

        //Instantiate rooms based on the final list of available coordinates
        foreach (Coord openCoord in currentOpenCoords)
        {


            Vector3 roomPosition = CoordToPosition(openCoord);
            //Transform newRoom = Instantiate(roomPrefab) as Transform;
            //Transform newRoom = Instantiate(rooms[UnityEngine.Random.Range(0, rooms.Length)]);
            Transform newRoom = Instantiate(roomPrefab);
            newRoom.GetComponent<Room>().Init();

            newRoom.SetParent(dungeonHolder, false);
            newRoom.localPosition = roomPosition;
            newRoom.localRotation = Quaternion.identity;
            newRoom.localScale = Vector3.one;

            //Find neighboors
            foreach (Coord open in currentOpenCoords)
            {
                Room openRoom = newRoom.GetComponent<Room>();
                if (openRoom != null)
                {
                    //If Top Neighboor
                    Coord neighborCoord = new Coord(openCoord.x, openCoord.y + 1);
                    
                    if (neighborCoord == open)
                    {
                        openRoom.AddNeighbor(Room.Direction.North, neighborCoord);
                        openRoom.SetHallwayActive(Room.Direction.North, true);
                    }

                    //If Right Neighboor
                    neighborCoord = new Coord(openCoord.x + 1, openCoord.y);
                    if (neighborCoord == open)
                    {
                        openRoom.AddNeighbor(Room.Direction.East, neighborCoord);
                        openRoom.SetHallwayActive(Room.Direction.East, true);
                    }

                    //If Bottom Neighboor
                    neighborCoord = new Coord(openCoord.x, openCoord.y - 1);
                    if (neighborCoord == open)
                    {
                        openRoom.AddNeighbor(Room.Direction.South, neighborCoord);
                        openRoom.SetHallwayActive(Room.Direction.South, true);
                    }

                    //If Left Neighboor
                    neighborCoord = new Coord(openCoord.x - 1, openCoord.y);
                    if (neighborCoord == open)
                    {
                        openRoom.AddNeighbor(Room.Direction.West, neighborCoord);
                        openRoom.SetHallwayActive(Room.Direction.West, true);
                        //createRoom.AddNeighbor(Room.Direction.West, neighborCoord);
                    }
                }
            }

            roomLayout[openCoord.x, openCoord.y] = newRoom;

            //ToDo Cleanup - Unload Resources and shizz
            Resources.UnloadUnusedAssets();
        }
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
