using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shift { 
    public class DungeonGen : MonoBehaviour
    {

        public Dungeon[] dungeons;
        public int dungeonIndex;

        public Transform RoomPrefab;

        private List<Coord> possibleRoomCoords;
        private Queue<Coord> shuffleRoomCoords;
        private Queue<Coord> shuffleOpenRoomCoords;

        private Transform[,] RoomMap;
        private Dungeon currentDungeon;



        // Use this for initialization
        void Start () {
	
	    }

        public void GenerateDungeon()
        {
            currentDungeon = dungeons[dungeonIndex];
            RoomMap = new Transform[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];
            System.Random prng = new System.Random(currentDungeon.seed);
            //TODO: May need room size multiplier
            GetComponent<BoxCollider>().size = new Vector3(currentDungeon.dungeonSize.x , .05f, currentDungeon.dungeonSize.y);

            //Generating Coordinates
            possibleRoomCoords = new List<Coord>();
            for (int x = 0; x < currentDungeon.dungeonSize.x; x++)
            {
                for (int y = 0; y < currentDungeon.dungeonSize.y; y++)
                {
                    possibleRoomCoords.Add((new Coord(x,y)));
                }
            }

            shuffleRoomCoords = new Queue<Coord>(Utility.ShuffleArray(possibleRoomCoords.ToArray(), currentDungeon.seed));

            //Create Dungeon Parent Object
            string holderName = "Generated Dungeon";
            if (transform.FindChild(holderName))
            {
                DestroyImmediate(transform.FindChild(holderName).gameObject);
            }

            Transform dungeonHolder = new GameObject(holderName).transform;
            dungeonHolder.parent = transform;

            //Spawn Rooms

            //
            bool[,] roomMap = new bool[(int)currentDungeon.dungeonSize.x, (int)currentDungeon.dungeonSize.y];

            int roomCount = (int) (currentDungeon.dungeonSize.x * currentDungeon.dungeonSize.y * currentDungeon.roomDensity);
            int currentRoomCount = roomCount;

            List<Coord> allOpenCoords = new List<Coord>(possibleRoomCoords);

            for (int i = 0; i < roomCount; i++)
            {
                Coord randomCoord = GetRandomCoord();
                roomMap[randomCoord.x, randomCoord.y] = false;
                currentRoomCount--;
                if (randomCoord != currentDungeon.dungeonCenter && MapIsAccessible(roomMap, currentRoomCount))
                {
                    Vector3 emptyRoomPosition = CoordToPosition(randomCoord.x, randomCoord.y);

                    allOpenCoords.Remove(randomCoord);
                }
                else
                {
                    roomMap[randomCoord.x, randomCoord.y] = true;
                    currentRoomCount++;
                }
            }


        }

        bool MapIsAccessible(bool[,] roomMap, int currentObstacleCount)
        {
            //Flood Fill to check for accessible counts
            bool[,] mapFlags = new bool[roomMap.GetLength(0), roomMap.GetLength(1)];
            Queue<Coord> queue = new Queue<Coord>();
            queue.Enqueue(currentDungeon.dungeonCenter);

            mapFlags[currentDungeon.dungeonCenter.x, currentDungeon.dungeonSize.y] = true;

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
                            if (neighborX >= 0 && neighborX < roomMap.GetLength(0) && neighborY >= 0 && neighborY < roomMap.GetLength(1))
                            {
                                if (!mapFlags[neighborX, neighborY] && !roomMap[neighborX, neighborY])
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

        public Coord GetRandomCoord()
        {
            Coord randomCoord = shuffleRoomCoords.Dequeue();
            shuffleRoomCoords.Enqueue(randomCoord);
            return randomCoord;
        }

        Vector3 CoordToPosition(int x, int y)
        {
            //TODO: May need room size multiplier
            return new Vector3(-currentDungeon.dungeonSize.x / 2f + 0.5f + x, 0, -currentDungeon.dungeonSize.y / 2f + 0.5f + y);
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
            #region Core
            //Defines the map size on a grid (i.e. 5x5)
            public Coord dungeonSize;

            //Defines how many rooms are available in the grid
            [Range(0,1)]
            public float roomDensity;

            //Seed the random nature of the map
            public int seed;

            //Indicates the "starting" room of a dungeon
            //Used to ensure all rooms can be reached
            public Coord dungeonCenter
            {
                get
                {
                    return new Coord(dungeonSize.x / 2, dungeonSize.y / 2);
                }
            }
            #endregion
            
            #region TBD
            //Contents of region are being debated in usefulness

            //List of Rooms
            

            #endregion
        }

        [System.Serializable]
        public class Room
        {
            //Base structure of a room
            //Requires:
            //  Spawner
            //  Puzzle
        }
    }
}