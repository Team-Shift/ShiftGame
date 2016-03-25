using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DimensionalSwitchManager : MonoBehaviour
{
    private List<GameObject> Enemies;
    private List<GameObject> Enviornment;
    public GameObject Player;
    private float playerY;

    public bool proSwitch = false;

    void Start()
    {
        Enemies = new List<GameObject>();
        Enviornment = new List<GameObject>();

        FindAllEnE();
        playerY = Player.transform.position.y;
    }

    void Update()
    {

    }

    //Finding all enemies and enviornment objects
    void FindAllEnE()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enviornment = GameObject.FindGameObjectsWithTag("Enviornment");

        foreach(GameObject enemy in enemies)
        {
            Enemies.Add(enemy);
        }

        foreach (GameObject enviorn in enviornment)
        {
            Enviornment.Add(enviorn);
        }

        Debug.Log(enemies.Length);
        Debug.Log(enviornment.Length);
    }

    public void Shift()
    {
        proSwitch = !proSwitch;

        if (proSwitch == false)
        {
            foreach (GameObject enemy in Enemies)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, playerY/* - (enemy.GetComponent<Collider>().bounds.size.y / 2)*/, enemy.transform.position.z);
            }

            foreach (GameObject enviorn in Enviornment)
            {
                enviorn.transform.position = new Vector3(enviorn.transform.position.x, playerY-1/* - (enemy.GetComponent<Collider>().bounds.size.y / 2)*/, enviorn.transform.position.z);
            }
        }

        else if(proSwitch == true)
        {
            foreach (GameObject enemy in Enemies)
            {                                                                   //Y position needs to be saved and brought back later on
                enemy.transform.position = new Vector3(enemy.transform.position.x, 2, enemy.transform.position.z);
            }
            foreach (GameObject enviorn in Enviornment)
            {
                enviorn.transform.position = new Vector3(enviorn.transform.position.x, 1/* - (enemy.GetComponent<Collider>().bounds.size.y / 2)*/, enviorn.transform.position.z);
            }
        }
    }
}
