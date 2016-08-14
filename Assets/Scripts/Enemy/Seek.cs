using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour
{
    public float maxVel;
    public bool shouldSeek;
    //GameObject player;
    public Vector3 objToSeek;

    private Vector2 targetPos;
    private Rigidbody rb;
    private Vector2 desiredVel;
    private Vector2 vel;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        shouldSeek = false;
        //player = GameObject.FindGameObjectWithTag("Player");
        objToSeek = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        if (shouldSeek)
        {
            targetPos = new Vector2(objToSeek.x, objToSeek.z);
            //targetPos = new Vector2(player.transform.position.x, player.transform.position.z);
            vel = new Vector3(rb.velocity.x , rb.velocity.z) * Time.deltaTime;

            Vector2 v = new Vector2(targetPos.x - gameObject.transform.position.x, targetPos.y - gameObject.transform.position.z);
			desiredVel = v * maxVel * Time.deltaTime;
			Vector2 steering = desiredVel - vel;

			gameObject.transform.LookAt (objToSeek, Vector3.up);

            steering = steering / rb.mass;

            rb.velocity += new Vector3(steering.x, 0, steering.y);
        }
        else
            rb.velocity = Vector3.zero;
    }

    public void SetObjToSeek(Vector3 v)
    {
        objToSeek = v;
    }
}
