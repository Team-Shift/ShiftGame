﻿using UnityEngine;
using System.Collections;

public class projectileShooting : MonoBehaviour {
    public float lifetime = 1.0f;
    public float speed = 10.0f;
	public float yOffset;

    private float startTime;

	void Start () {
        startTime = Time.time;
	}
	
	void Update ()
    {
		Vector3 v = new Vector3(-speed * Time.deltaTime, yOffset,0);

        gameObject.transform.Translate(v);

        if(Time.time > startTime +lifetime)
        {
            Destroy(gameObject);
        }
	}

}
