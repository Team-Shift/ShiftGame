using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveController : MonoBehaviour
{
    public static SaveController saveController;

    public Custom2DController player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Custom2DController>();

        if(saveController == null)
        {
            saveController = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(saveController != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "saveData.dat");

        PlayerData data = new PlayerData();
        data.health = player.Health;
        data.posX = player.gameObject.transform.position.x;
        data.posY = player.gameObject.transform.position.y;
        data.posZ = player.gameObject.transform.position.z;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "saveData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            player.Health = data.health;
            player.gameObject.transform.position = new Vector3(data.posX, data.posY, data.posZ);
        }
    }
}

[Serializable]
class PlayerData
{
    public int health { get; set; }
    public float posX { get; set; }
    public float posY { get; set; }
    public float posZ { get; set; }
}
