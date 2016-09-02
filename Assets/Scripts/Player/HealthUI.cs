using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour {

    //UI
    public GUITexture HeartFillTexture;         // filled heart
    public GUITexture HeartContainerTexture;    // empty heart
    private List<GUITexture> HeartFillList = new List<GUITexture>();
    private List<GUITexture> HeartContainerList = new List<GUITexture>();
    int AmountOfHeartContainer;
    public float XOffset = 0.14f;
    public float YOffset = 0.92f;

    int Health;

    // Use this for initialization
    void Start () {
        // UI STUFF
        Health = gameObject.GetComponent<PlayerCombat>().Health;
        AmountOfHeartContainer = Health;
        SpawnHeart(Health);
        SpawnHeartContainer(AmountOfHeartContainer);
    }

    public void SpawnHeart(int HeartAmount)
    {
        for (int i = 0; i < HeartAmount; i++)
        {
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector3(i * XOffset + .07f, YOffset, 2), Quaternion.identity)));
        }

        // ToDo This is bad get rid of it as well
        foreach (var texture in HeartFillList)
        {
            DontDestroyOnLoad(texture);
        }
    }

    public void SpawnHeartContainer(int HeartAmount)
    {

        for (int i = 0; i < HeartAmount; i++)
        {
            HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }

        // ToDo This is bad get rid of it as well
        foreach (var texture in HeartContainerList)
        {
            DontDestroyOnLoad(texture);
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
        if (HeartFillList.Count < HeartContainerList.Count)
        {
            //Health++;
            int HeartFillIndex = HeartFillList.Count;
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector3(HeartFillIndex * XOffset + .07f, YOffset, 2), Quaternion.identity)));
        }
    }

    public void DamageHeart()
    {
		if ((HeartFillList.Count - 1) > 0) {
			Destroy (HeartFillList [HeartFillList.Count - 1].gameObject);
		}
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }

    public void RemoveHeartContainer()
    {
        AmountOfHeartContainer--;
        Destroy(HeartContainerList[HeartContainerList.Count - 1].gameObject);
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }
}
