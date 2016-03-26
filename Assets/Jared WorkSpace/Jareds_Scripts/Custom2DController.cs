using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class Custom2DController : MonoBehaviour
{
    public GameObject player;
    public GameObject rangedTemp;
    public GameObject meleeWeapon;
    public GameObject mousePointer_Debug;
    private GameObject sword;
    public float turnSpeed = 180f;
    public float speed = 6.0f;
    public float playerGroundLevel = 0;
    [HideInInspector]
    private Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public int health = 3;
    [HideInInspector]
    public bool CameraSwitch = false;
    private Animator anim;
    
    public enum FacingDirection { Forward, Backward, Left, Right };
    public FacingDirection playerDir;
    public enum CurrentItemType { Melee, Range, Scroll, Spell, None};
    public CurrentItemType currentHeld;
    private DimensionalSwitchManager manager;
    //add private reference of camera change


    // Use this for initialization
    void Start()
    {
        playerDir = FacingDirection.Forward;
        currentHeld = CurrentItemType.None;
        //playerGroundLevel = playerGroundLevel + player.GetComponent<Collider>().bounds.size.y / 2;
        manager = GameObject.FindObjectOfType<DimensionalSwitchManager>();
        anim = player.GetComponent<Animator>();

        CameraSwitch = false;
        //Make sure to finish init by finding it

    }

    // Update is called once per frame
    void Update()
    {
        if (CameraSwitch == false)
        {
            Move2D();
        }
        else if (CameraSwitch == true)
        {
            Move3D();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            MeleeAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RangedAttack();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraSwitch = !CameraSwitch;
            manager.Shift();
            //last call on LShift down
        }
    }

    void Move2D()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(moveDirection.x != 0 || moveDirection.z != 0)
        {
            anim.SetBool("walk", true);
        }
        else if(moveDirection.x == 0 || moveDirection.z == 0)
        {
            anim.SetBool("walk", false);
        }

        transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);

        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("walk", true);
            playerDir = FacingDirection.Forward;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 0.0f , player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("walk", true);
            playerDir = FacingDirection.Left;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 270.0f, player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("walk", true);
            playerDir = FacingDirection.Backward;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180.0f, player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("walk", true);
            playerDir = FacingDirection.Right;
            //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 90.0f, player.transform.eulerAngles.z);
            player.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
    }

    void Move3D()
    {
        //Vector3 cameraWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        //cameraWorldPos.y = playerGroundLevel;

        //Debug.Log("Current mouse position = " + cameraWorldPos.x + ", " + cameraWorldPos.y + ", " + cameraWorldPos.z);
        //mousePointer_Debug.transform.position = cameraWorldPos;

        //Vector3 direction = (cameraWorldPos - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        
        //player.transform.rotation = lookRotation;

        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        //player.transform.Translate(0, 0, vertical);

        //Start of new code

        float forwardBack = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float turning = Input.GetAxis("Mouse X");

        player.transform.Translate(Vector3.forward * forwardBack + Vector3.right * strafe, Space.Self);

        player.transform.Rotate(Vector3.up * turning * turnSpeed * Time.deltaTime);
        //end of new code


        if (vertical != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }


        /*
        ||==================================================================================================||
        || Note: The below if statement will prevent the player from being able to jump so if you're adding ||
        ||       jump then don't forget to fix the last issue of the player being able to fly by backing up.||
        ||==================================================================================================||
        */
        if(player.transform.position.y > playerGroundLevel)
        {
            player.transform.position = new Vector3(player.transform.position.x, playerGroundLevel, player.transform.position.z);
        }
    }

    public void DamageFallback(Vector3 damageSource)
    {
        health--;
        Debug.Log(health);

        if(player.transform.position.z < damageSource.z)
        {
            player.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 1000);
            //player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + -Vector3.forward * 2, 0.05f);
            Debug.Log("Greater Z");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if(player.transform.position.z > damageSource.z)
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.forward * 1000);
            //player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + Vector3.forward * 2, 0.05f);
            Debug.Log("Lower Z");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (player.transform.position.x < damageSource.x)
        {
            player.GetComponent<Rigidbody>().AddForce(-Vector3.right * 1000);
            //player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + -Vector3.right * 2, 0.05f);
            Debug.Log("Greater X");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (player.transform.position.x > damageSource.x)
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
            //player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + Vector3.right * 2, 0.05f);
            Debug.Log("Lower X");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void MeleeAttack()
    {
        //Debug.Log("Player swung their sword");
        if (CameraSwitch == true)
        {
            anim.SetTrigger("3D_sword_attack");
        }
        else
        {
            anim.SetTrigger("sword_attack");
        }

        //run animation for sword swinging
        //Add script for damage on the weapon itself maybe?
    }

    void RangedAttack()
    {
        anim.SetTrigger("bow_attack");
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
