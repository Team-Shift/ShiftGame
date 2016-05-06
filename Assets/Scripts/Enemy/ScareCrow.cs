using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScareCrow : MonoBehaviour
{
    public Custom2DController player;
    public float xOffset = 0;
    public float zOffset = 0;

    //For telling the boss where he should be/ teleport to
    [HideInInspector]
    public List<Vector3> pathList;
    public Vector3 center;
    public bool showPath;
    public float teleportTime = 0;
    //====================================================

    //For instantiating the ghosts
    public GameObject ghost;
    float speedMulti = 1.0f;
    float rangeMulti = 1.0f;
    float shootInterval = 1.0f;
    float baseX = 0.0f;
    float shootTimeAC = 1.0f;
    //============================

    private bool canShoot = false;


    // Use this for initialization
    void Start()
    {
        baseX = transform.position.x;

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name == "pathNode")
            {
                pathList.Add(t.position);
            }
            if(t.name == "RoomCenter")
            {
                center = t.position;
            }
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Custom2DController>();
        gameObject.transform.position = pathList[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(teleportTime <= 0)
        {
            Teleport();
            teleportTime += 5;
            //gameObject.transform.eulerAngles = gameObject.transform.rotation.eulerAngles + 180f * Vector3.up;
        }
        else
        {
            teleportTime -= Time.deltaTime;
        }

        //LinearShot();
        //OscillateTowardsPlayer();
        SpawnGhosts_Tri();
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
        //Right -> Left
        if (gameObject.transform.position == pathList[0])
        {
            gameObject.transform.position = pathList[1];
        }
        //Left -> Top
        else if (gameObject.transform.position == pathList[1])
        {
            gameObject.transform.position = pathList[2];
        }
        //Top -> Bottom
        else if (gameObject.transform.position == pathList[2])
        {
            gameObject.transform.position = pathList[3];
        }
        //Bottom -> Right
        else if (gameObject.transform.position == pathList[3])
        {
            gameObject.transform.position = pathList[0];
        }

        //Vector3 playerYPos = new Vector3(player.gameObject.transform.position.x, transform.position.y, player.gameObject.transform.position.z);
        transform.LookAt(center);
    }

    private void ShotPattern()
    {
        //Just a placeholder till I have other stuff done
    }

    private void LinearShot()
    {
        //Set up variables before anything else
        speedMulti = 1.0f;
        rangeMulti = 2.0f;
        shootInterval = 0.15f;

        //Get positions for spawning and interval between shots
        Vector3 position = transform.position;
        float interval = Mathf.Sin(Time.time * (speedMulti / rangeMulti)) * rangeMulti;
        bool shoot = false;

            if (Time.deltaTime + shootTimeAC > shootInterval)
            {
                shootTimeAC = 0.0f;
                shoot = true;
            }

            else
            {
                shootTimeAC += Time.deltaTime;
            }

            if (shoot)
            {
                GameObject shot = Instantiate(ghost, transform.position, ghost.transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
            }
        
    }

    private void OscillateTowardsPlayer()
    {
        //Oscillate(0, 180); //Player left of boss
        //Oscillate(270, 450); //Player ahead of boss
        //Oscillate(90, 270); //Player behind boss
        //Oscillate(180, 360); //Player right of boss

        //Right
        if (gameObject.transform.position == pathList[0])
        {
            Oscillate(180, 360);
        }
        //Left
        else if (gameObject.transform.position == pathList[1])
        {
            Oscillate(0, 180);
        }
        //Top
        else if (gameObject.transform.position == pathList[2])
        {
            Oscillate(90, 270);
        }
        //Bottom
        else if (gameObject.transform.position == pathList[3])
        {
            Oscillate(270, 450);
        }
    }

    private void Oscillate(float fromAngle, float toAngle)
    {
        Vector3 from = new Vector3(0, fromAngle, 0);
        Vector3 to = new Vector3(0, toAngle, 0);
        float speed = 0.3f;

        //float t = (Mathf.Sin(Time.time * speed * Mathf.PI * 2.0f) /2.0f);
        float t = Mathf.PingPong(Time.time * speed * 2.0f, 1.0f);
        transform.eulerAngles = Vector3.Lerp(from, to, t);
    }

    private void SpawnGhosts_Tri()
    {
        //Set up variables before anything else
        speedMulti = 1.0f;
        rangeMulti = 2.0f;
        shootInterval = 0.5f;

        //Get positions for spawning and interval between shots
        Vector3 position = transform.position;
        float interval = Mathf.Sin(Time.time * (speedMulti / rangeMulti)) * rangeMulti;
        bool shoot = false;

        if (Time.deltaTime + shootTimeAC > shootInterval)
        {
            shootTimeAC = 0.0f;
            shoot = true;
        }

        else
        {
            shootTimeAC += Time.deltaTime;
        }

        if (shoot)
        {
            //forward
            GameObject ghost1 = Instantiate(ghost, transform.position, transform.rotation) as GameObject;
            ghost1.GetComponent<Rigidbody>().AddForce(ghost1.transform.forward * 200);
            //left
            GameObject ghost2 = Instantiate(ghost, transform.position, Quaternion.Euler(0, transform.rotation.y - 10, 0)) as GameObject;
            ghost2.GetComponent<Rigidbody>().AddForce(ghost2.transform.forward * 200);
            //right
            GameObject ghost3 = Instantiate(ghost, transform.position, Quaternion.Euler(0, transform.rotation.y + 10, 0)) as GameObject;
            ghost3.GetComponent<Rigidbody>().AddForce(ghost3.transform.forward * 200);
        }
    }


    IEnumerator Wait(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

}
