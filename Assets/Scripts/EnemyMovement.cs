using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    
    public float maxVel = 50;
    public bool shouldSeek;

    private Vector2 targetPos;
    private Rigidbody rb;
    private Vector2 desiredVel;
    private Vector2 vel;
    private GameObject player;


    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        shouldSeek = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () {
        if (shouldSeek)
        {
            targetPos = new Vector2(player.transform.position.x, player.transform.position.z);
            vel = new Vector3(rb.velocity.x, rb.velocity.z);
            Seek();
        }
	}

    // should be different 3D 2D
    void Wander()
    {
        // 2D back and forth

        // 3D wander
    }

    void Seek()
    {
        Vector2 v = new Vector2(targetPos.x - gameObject.transform.position.x, targetPos.y - gameObject.transform.position.z);
        desiredVel = v * maxVel * Time.deltaTime;

        Vector2 steering = desiredVel - vel;

        gameObject.transform.LookAt(new Vector3(targetPos.x, 0, targetPos.y), Vector3.up);

        Debug.Log(vel);

        steering = steering / rb.mass;

        rb.velocity += new Vector3(steering.x, 0, steering.y);

        

        //gameObject.transform.position += rb.velocity;
        
      ///  gameObject.transform.position = ;   
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            shouldSeek = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldSeek = false;
            rb.velocity = Vector3.zero;
        }
    }
}
