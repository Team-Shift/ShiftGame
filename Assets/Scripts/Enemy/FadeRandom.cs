using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeRandom : MonoBehaviour {
    List<Material> meshMat;
	List<Renderer> meshRend;

    public float durationInvisibe;
    public float invisibleInterval;

	void Start ()
    {
        meshMat = new List<Material>();
		meshRend = new List<Renderer> ();
	    foreach(Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            meshMat.Add(r.material);
			meshRend.Add (r);
        }
		for (int i = 0; i < meshMat.Count; i++) {
			meshMat [i].SetFloat ("_Mode", 3.0f);
			Color c = meshMat [i].color;
			c.a = .1f;

			Material mat = new Material(Shader.Find("Transparent/Diffuse"));
			mat.color = c;
			mat.SetFloat("_Mode", 3.0f);
			meshMat [i] = mat;
			//meshMat [i].SetColor ("_Color", c);
		}
	}
	
	void Update ()
    {
		for (int i = 0; i < meshMat.Count; i++) {
			if (meshMat [i].color.a < 255) {
				Color c = meshMat[i].color;
				c.a += .01f;
				//meshMat[i].SetColor("_Color", c);
				//meshRend [i].material = meshMat [i];
			}
		}
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
