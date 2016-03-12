using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Custom2DController : MonoBehaviour
{
    public GameObject player;
    public GameObject rangedTemp;
    public GameObject meleeWeapon;
    public float turnSpeed = 180f;
    public float speed = 6.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    private Rigidbody body;
    [HideInInspector]
    public int health = 3;
    
    public enum FacingDirection { Forward, Backward, Left, Right };
    public FacingDirection playerDir;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        playerDir = FacingDirection.Forward;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            MeleeAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RangedAttack();
        }
    }

    void Move()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);


        if(Input.GetKeyDown(KeyCode.W))
        {
            playerDir = FacingDirection.Forward;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 0.0f , player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerDir = FacingDirection.Left;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 270.0f, player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerDir = FacingDirection.Backward;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180.0f, player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerDir = FacingDirection.Right;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90.0f, player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
    }

    void DamageFallback()
    {
        health--;

        //Push the player back in the opposite direction from damage source
        transform.Translate(new Vector3());
    }

    void MeleeAttack()
    {
        Debug.Log("Player swung their sword");
    }

    void RangedAttack()
    {
        if(playerDir == FacingDirection.Forward)
        {
            GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.0f), Quaternion.identity) as GameObject;
            projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
        }
        else if (playerDir == FacingDirection.Backward)
        {
            GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 1.0f), Quaternion.identity) as GameObject;
            projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
        }
        else if (playerDir == FacingDirection.Left)
        {
            GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x - 1.0f, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
            projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
        }
        else if (playerDir == FacingDirection.Right)
        {
            GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x + 1.0f, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
            projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
        }
    }
}
