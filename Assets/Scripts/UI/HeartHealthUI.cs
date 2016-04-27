using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartHealthUI : MonoBehaviour {

    public GUITexture HeartFillTexture;
    public GUITexture HeartContainerTexture;
    private List<GUITexture> HeartFillList = new List<GUITexture>();
    private List<GUITexture> HeartContainerList = new List<GUITexture>();
    public int Health;
    int AmountOfHeartContainer;
    public float XOffset = 0.14f;
    public float YOffset = 0.92f;
    private int LoopYOffset;
    private bool Loop = false;


    // Use this for initialization
    void Start()
    {
        AmountOfHeartContainer = Health;
        SpawnHeart(Health);
        SpawnHeartContainer(AmountOfHeartContainer);
    }

    void SpawnHeart(int HeartAmount)
    {
        for (int i = 0; i < Health; i++)
        {
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }

    void SpawnHeartContainer(int HeartAmount)
    {

        for (int i = 0; i < Health; i++)
        {
            HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }


    public void AddHeartContainer()
    {
        AmountOfHeartContainer++;
        int HeartContainerIndex = HeartContainerList.Count;
            HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector3(HeartContainerIndex * XOffset + .07f, YOffset, -1), Quaternion.identity)));
    }

    public void HealHeart()
    {
        if (HeartFillList.Count < HeartContainerList.Count) {
            Health++;
            int HeartFillIndex = HeartFillList.Count;
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector2(HeartFillIndex * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }

    public void DamageHeart()
    {
        Health--;
        Destroy(HeartFillList[HeartFillList.Count - 1].gameObject);
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }

    public void RemoveHeartContainer()
    {
        AmountOfHeartContainer--;
        Destroy(HeartContainerList[HeartContainerList.Count - 1].gameObject);
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }
}
