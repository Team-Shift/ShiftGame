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
        }
        else
        {
            teleportTime -= Time.deltaTime;
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
        if(gameObject.transform.position == pathList[0])
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

    }
}
