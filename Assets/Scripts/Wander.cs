using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wander : MonoBehaviour {
    
    public List<Vector3> pathList;
    public float speed = 2;
    private int index = 0;
    public bool showPath;
    public bool shouldWander;

    void Start () {
        if (!Application.isPlaying) return;
        showPath = true;
        shouldWander = true;
	}

    void Update() {
        if (shouldWander)
        {
            Vector3 dir = pathList[index] - gameObject.transform.position;
            // go to path node
            gameObject.transform.position += (dir.normalized) * Time.deltaTime * speed;
            //rotate to path node
            gameObject.transform.LookAt(pathList[index]);

            // increment index
            if (dir.magnitude <= 1.0f) index++;
            if (index >= pathList.Count) index = 0;
        }
	}

    void OnDrawGizmos()
    {
        if (showPath)
        {
            foreach (Vector3 v in pathList)
            {
                Gizmos.DrawWireSphere(v, 0.2f);
            }
        }
    }
}
