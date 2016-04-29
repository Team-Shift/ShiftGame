using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeRandom : MonoBehaviour {
    List<Material> meshShaders;

    public float durationInvisibe;
    public float invisibleInterval;

	void Start ()
    {
        meshShaders = new List<Material>();
	    foreach(Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            meshShaders.Add(r.material);
        }
	}
	
	void Update ()
    {
	    // call disappear every interval

        // reappear after duration
	}

    void Disappear()
    {

    }

    void Reappear()
    {

    }
}
