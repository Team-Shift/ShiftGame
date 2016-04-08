using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wander : MonoBehaviour {

    [HideInInspector]
    public List<Vector3> pathList;
    //[HideInInspector]
    public bool shouldWander;
    public float speed = 2;
    public bool showPath;
    public bool isFloatingEnemy;
    
    private int index;

    void Start () {

        if(isFloatingEnemy)
            //gameObject.transform.Translate(0, 1, 0);
        // get all child game objects with name pathNode
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name == "pathNode")
            {
                pathList.Add(t.position);
                t.hideFlags = HideFlags.HideInHierarchy;
            }
        }
        
        if (!Application.isPlaying) return;
        showPath = true;
        shouldWander = true;
        index = 0;
    }

    void Update() {
        showPath = false;
        if (shouldWander && pathList.Count != 0)
        {
            Vector3 dir = pathList[index] - gameObject.transform.position;
            // go to path node
            gameObject.transform.position += (dir.normalized) * Time.deltaTime * speed;
            //rotate to path node
            gameObject.transform.LookAt(pathList[index]);
            
            //Debug.Log(dir.magnitude);
            // increment index
            if (dir.magnitude <= 1.0f) index++;
            if (index >= pathList.Count) index = 0;
        }
	}

    void OnDrawGizmos()
    { 
        if(showPath)
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
        //nodeList.Add(g);
    }

    void OnTriggerEnter(Collider other)
    {
        // only fire if collider is player
        if (other.tag == "Player")
        {
            shouldWander = false;
        }
        // lock y
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldWander = true;
        }
    }
}
