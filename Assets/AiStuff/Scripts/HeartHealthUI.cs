using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartHealthUI : MonoBehaviour {

    public GUITexture HeartFillTexture;
    public GUITexture HeartContainerTexture;
    private List<GUITexture> HeartFillList = new List<GUITexture>();
    private List<GUITexture> HeartContainerList = new List<GUITexture>();
    public int AmountOfHearts;
    private float YOffset = 0.92f;
    private float XOffset = 0.14f;


    // Use this for initialization
    void Start()
    {

        SpawnHeart(AmountOfHearts);
        SpawnHeartContainer(AmountOfHearts);
    }

    void SpawnHeart(int HeartAmount)
    {
        for (int i = 0; i < AmountOfHearts; i++)
        {
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }

    void SpawnHeartContainer(int HeartAmount)
    {
        for (int i = 0; i < AmountOfHearts; i++)
        {
            HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }

    public void AddHeart()
    {

        AmountOfHearts++;
        int i = HeartContainerList.Count;
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
            HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
    }

    public void RemoveHeart()
    {
        AmountOfHearts--;
        Destroy(HeartFillList[HeartFillList.Count - 1].gameObject);
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }

}
