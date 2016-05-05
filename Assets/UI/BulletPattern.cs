using UnityEngine;
using System.Collections;

public class BulletPattern : MonoBehaviour {

    public int pattern = 0;
	
	// Update is called once per frame
	void Update () {
        if (pattern == 0)
        {
            Quaternion rot = Quaternion.Euler(0, Mathf.Sin(Time.time) * 180, 0);
            this.transform.rotation = rot;
        }
        if(pattern == 1)
        {
            Quaternion rot = Quaternion.Euler(Mathf.Sin(Time.time), Mathf.Cos(Time.time) * 270, 0);
            this.transform.rotation = rot;
        }
        if(pattern == 2)
        {
            Quaternion rot = Quaternion.Euler(0, Mathf.Sin(Time.time) * 180, 0);
            this.transform.rotation = rot;
        }

    }
}
