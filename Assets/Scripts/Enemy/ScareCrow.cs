using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScareCrow : MonoBehaviour
{
    public Custom2DController player;
    public GameObject ghost;

    [HideInInspector]
    public List<Vector3> pathList;
    public bool showPath;

    float teleportTime = 0;
    float spawnTime = 0;

    // Use this for initialization
    void Start()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name == "pathNode")
            {
                pathList.Add(t.position);
            }
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Custom2DController>();
        gameObject.transform.position = pathList[0];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(teleportTime);
        if(teleportTime <= 0)
        {
            Teleport();
            teleportTime += 20;
            gameObject.transform.eulerAngles = gameObject.transform.rotation.eulerAngles + 180f * Vector3.up;
            SpawnGhosts();
        }
        else
        {
            teleportTime -= Time.deltaTime;
        }

        if(spawnTime <= 0)
        {
            spawnTime += 5;
            SpawnGhosts();
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        if (showPath)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t.name == "pathNode")
                    Gizmos.DrawWireSphere(t.position, 0.2f);
            }
        }
    }

    public GameObject AddNode()
    {
        GameObject g = new GameObject();
        g.transform.SetParent(gameObject.transform);
        g.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        g.name = "pathNode";
        return g;
    }

    private void Teleport()
    {
        spawnTime += 5;

        if (gameObject.transform.position == pathList[0])
        {
            gameObject.transform.position = pathList[1];
        }
        else
        {
            gameObject.transform.position = pathList[0];
        }
    }

    private void SpawnGhosts()
    {
        GameObject ghost1 = Instantiate(ghost, gameObject.transform.position + gameObject.transform.forward, gameObject.transform.rotation) as GameObject;
        ghost1.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
        GameObject ghost2 = Instantiate(ghost, gameObject.transform.position + gameObject.transform.forward + (-gameObject.transform.right * 2), gameObject.transform.rotation) as GameObject;
        ghost2.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
        GameObject ghost3 = Instantiate(ghost, gameObject.transform.position + gameObject.transform.forward + (gameObject.transform.right * 2), gameObject.transform.rotation) as GameObject;
        ghost3.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
    }
}
