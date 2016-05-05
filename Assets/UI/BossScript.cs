using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

    public GameObject target;
    public GameObject bullet;
    public int interval = 2;
    

    float counter = 0;


    void Update()
    {
        counter++;
        //Quaternion rot = Quaternion.Euler(0, Mathf.Sin(Time.time) * 180, 0);
        //player.transform.rotation = rot;
        if(counter > interval)
        {
            GameObject bul = (GameObject)Instantiate(bullet, this.transform.position, Quaternion.identity);
            //bul.transform.LookAt(player.transform);
            bul.gameObject.GetComponent<Rigidbody>().AddForce(target.transform.forward * 1000);
            counter = 0;
        }
    }


}
