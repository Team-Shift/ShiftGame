using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Shift
{
    [RequireComponent(typeof (BoxCollider))]
    public class DungeonGen : MonoBehaviour
    {
        private Dungeon currentDungeon;
        public int dungeonIndex;

        public Dungeon[] dungeons;

        private List<Coord> possibleRoomCoords;

        private Transform[,] RoomMap;

        public Transform RoomPrefab;

        public float roomSize;
        private Queue<Coord> shuffleOpenRoomCoords;
        private Queue<Coord> shuffleRoomCoords;
        public Transform TilePrefab;

        private float roomWidth;

        private void Reset()
        {
        }


        // Use this for initialization
        private void Start()
        {

        }

        public void GenerateDungeon()
        {
            currentDungeon = dungeons[dungeonIndex];
            RoomMap = new Transform[currentDungeon.dungeonSize.x, currentDungeon.dungeonSize.y];
            var prng = new Random(currentDungeon.seed);

            //TODO: May need room size multiplier
            GetComponent<BoxCollider>().size = new Vector3(currentDungeon.dungeonSize.x*roomSize, .05f,
                currentDungeon.dungeonSize.y*roomSize);

            //Generating Coordinates
            possibleRoomCoords = new List<Coord>();
            for (var x = 0; x < currentDungeon.dungeonSize.x; x++)
            {
                for (var y = 0; y < currentDungeon.dungeonSize.y; y++)
                {
                    possibleRoomCoords.Add((new Coord(x, y)));
                }
            }

            shuffleRoomCoords = new Queue<Coord>(Utility.ShuffleArray(possibleRoomCoords.ToArray(), currentDungeon.seed));

            //Create Dungeon Parent Object
            var holderName = "Generated Dungeon";
            if (transform.FindChild(holderName))
            {
                DestroyImmediate(transform.FindChild(holderName).gameObject);
            }

            var dungeonHolder = new GameObject(holderName).transform;
            dungeonHolder.SetParent(transform, false);

            //Spawn Tiles
            for (var x = 0; x < currentDungeon.dungeonSize.x; x++)
            {
                for (var y = 0; y < currentDungeon.dungeonSize.y; y++)
                {
                    var tilePosition = CoordToPosition(x, y);
                    var newTile = Instantiate(TilePrefab);
                    newTile.SetParent(dungeonHolder, false);
                    newTile.localPosition = tilePosition;
                    newTile.localRotation = Quaternion.Euler(90, 0, 0);
                    newTile.localScale = Vector3.one * roomSize;
                    RoomMap[x, y] = newTile;
                }
            }


            //Spawn Rooms
            bool[,] roomMap = new bool[(int)currentDungeon.dungeonSize.x, (int)currentDungeon.dungeonSize.y];

            int roomCount = (int)(currentDungeon.dungeonSize.x * currentDungeon.dungeonSize.y * currentDungeon.roomDensity);
            int currentRoomCount = roomCount;

            List<Coord> allOpenCoords = new List<Coord>(possibleRoomCoords);

            for (int i = 0; i < roomCount; i++)
            {
                Coord randomCoord = GetRandomCoord();
                roomMap[randomCoord.x, randomCoord.y] = false;
                currentRoomCount++;
                if (randomCoord != currentDungeon.dungeonCenter && MapIsAccessible(roomMap, currentRoomCount))
                {
                    Vector3 emptyRoomPosition = CoordToPosition(randomCoord.x, randomCoord.y);

                    allOpenCoords.Remove(randomCoord);
                }
                else
                {
                    roomMap[randomCoord.x, randomCoord.y] = true;
                    currentRoomCount--;
                }
            }

            shuffleOpenRoomCoords = new Queue<Coord>(Utility.ShuffleArray(allOpenCoords.ToArray(), currentDungeon.seed));
        }

        private bool MapIsAccessible(bool[,] roomMap, int currentObstacleCount)
        {
            //Flood Fill to check for accessible counts
            var mapFlags = new bool[roomMap.GetLength(0), roomMap.GetLength(1)];
            var queue = new Queue<Coord>();
            queue.Enqueue(currentDungeon.dungeonCenter);

            mapFlags[currentDungeon.dungeonCenter.x, currentDungeon.dungeonSize.y] = true;

            var accessibleTileCount = 1;

            while (queue.Count > 0)
            {
                var tile = queue.Dequeue();

                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        var neighborX = tile.x + x;
                        var neighborY = tile.y + y;
                        if (x == 0 || y == 0)
                        {
                            if (neighborX >= 0 && neighborX < roomMap.GetLength(0) && neighborY >= 0 &&
                                neighborY < roomMap.GetLength(1))
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

            var targetAccessibleTileCount = currentDungeon.dungeonSize.x*currentDungeon.dungeonSize.y -
                                            currentObstacleCount;
            return targetAccessibleTileCount == accessibleTileCount;
        }

        public Coord GetRandomCoord()
        {
            var randomCoord = shuffleRoomCoords.Dequeue();
            shuffleRoomCoords.Enqueue(randomCoord);
            return randomCoord;
        }

        private Vector3 CoordToPosition(int x, int y)
        {
            //TODO: May need room size multiplier
            return new Vector3(-currentDungeon.dungeonSize.x/2f + 0.5f + x, 0, -currentDungeon.dungeonSize.y/2f + 0.5f + y) * roomSize;
        }

        [Serializable]
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

        [Serializable]
        public class Dungeon
        {
            #region Core

            //Defines the map size on a grid (i.e. 5x5)
            public Coord dungeonSize;

            //Defines how many rooms are available in the grid
            [Range(0, 1)] public float roomDensity;

            //Seed the random nature of the map
            public int seed;

            //Indicates the "starting" room of a dungeon
            //Used to ensure all rooms can be reached
            public Coord dungeonCenter
            {
                get { return new Coord(dungeonSize.x/2, dungeonSize.y/2); }
            }

            #endregion

            #region TBD

            //Contents of region are being debated in usefulness

            //List of Rooms

            #endregion
        }

        [Serializable]
        public class Room
        {
            //Base structure of a room
            //Requires:
            //  Spawner
            //  Puzzle
        }
    }
}