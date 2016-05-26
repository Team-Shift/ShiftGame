﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

    private Room[,] map;
    private Image[,] miniMap;

    private int roomWidth;
    private int roomHeight;

    public Custom2DController playerController;

    // Use this for initialization
    void Start ()
    {
        roomWidth = 15;
        roomHeight = 15;
        playerController = GameObject.FindWithTag("Player").GetComponent<Custom2DController>();

        InitMap();
	}

	void LateUpdate ()
	{
        miniMap[(int)playerController.playerMapPosition.x, (int)playerController.playerMapPosition.y].color = Color.red;
    }

    //ToDo Apply Proper positioning of the map UI Element
    void InitMap()
    {
        map = GetComponentInParent<MapGenerator>().map;
        miniMap = new Image[map.GetLength(0), map.GetLength(1)];
        int MaxX = map.GetLength(0);
        int MaxY = map.GetLength(1);

        for (int x = 0; x < MaxX; x++)
        {
            for (int y = 0; y < MaxY; y++)
            {
                if (map[x, y] != null)
                {
                    Image roomSprite = new GameObject("room" + x + "," + y, typeof (Image)).GetComponent<Image>();
                    roomSprite.transform.SetParent(this.transform);
                    roomSprite.rectTransform.sizeDelta = new Vector2(roomWidth, roomHeight);
                    roomSprite.rectTransform.pivot = Vector2.zero;
                    roomSprite.rectTransform.position = new Vector3(x*roomWidth*2 + 800, y*roomHeight*2);

                    miniMap[x, y] = roomSprite;
                }
                else
                {
                    Debug.Log("Value is null at" + x + y);
                }
            }
        }
    }
}
