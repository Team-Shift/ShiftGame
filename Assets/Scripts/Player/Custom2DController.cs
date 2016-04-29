using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Custom2DController : MonoBehaviour
{
    public GameObject player;
    private GameObject pivotPoint;
    
    public float turnSpeed = 180f;
    public float speed = 6.0f;
    [HideInInspector]
    private Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public bool CameraSwitch = false;
    private Animator anim;

    
    public float pushUpForce = 10;
    private bool jump = true;
    public float jumpTimeLeft = 1f;
    
    //Movement
    public enum FacingDirection { Forward, Backward, Left, Right };
    public FacingDirection playerDir;

    public GameObject dust;

    //Combat

    MenuManager sceneShit;


    // Use this for initialization
    void Start()
    {
        playerDir = FacingDirection.Forward;
        //currentHeld = CurrentItemType.None;
        anim = player.GetComponent<Animator>();
        CameraSwitch = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        pivotPoint = GameObject.Find("PivotPoint");

        
        sceneShit = FindObjectOfType<MenuManager>();

        

    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.y <= -10)
        {
            anim.SetFloat("DeathIndex", 1);
            anim.SetTrigger("Death");
            this.transform.LoadScene(1);
            //sceneShit.TownScene();
        }

        //Movement
        if (CameraSwitch == false)
        {
            Move2D();
        }
        else if (CameraSwitch == true)
        {
            Move3D();
        }

        

        //Shift
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            CameraSwitch = !CameraSwitch;
            //manager.Shift();
            if (CameraSwitch == false)
            {         
                    player.layer = LayerMask.NameToLayer("AvoidLight2D");
            }
            else
            {
                Debug.Log("Changing players layer to default");
                player.layer = LayerMask.NameToLayer("Default");
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (jump == false)
        {
            jumpTimeLeft = jumpTimeLeft - Time.deltaTime;
        }

        if(jumpTimeLeft <= 0)
        {
            jumpTimeLeft = 1f;
            jump = true;
        }
    }

    /*
    * Movement
    */
    void Move2D()
    {
        //SO I CAN MOVE MY GODDAMN MOUSE IN 2D DON'T FUCKING COMMENT IT OUT FUCKERS IT DOESNT MAKE THE GAME ANY WORSE THEN IT ALREADY IS >:U 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(moveDirection.x != 0 || moveDirection.z != 0)
        {
            //Setting Y parameter to 1        Y Parameter 0 = Idle  1 = Walk
            anim.SetFloat("y", 1);
            //anim.SetBool("walk", true);
        }
        else if(moveDirection.x == 0 || moveDirection.z == 0)
        {
            anim.SetFloat("y", 0);
            //anim.SetBool("walk", false);
        }

        transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetFloat("y", 1);
            //anim.SetBool("walk", true);
            playerDir = FacingDirection.Forward;
            player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetFloat("y", 1);
            //anim.SetBool("walk", true);
            playerDir = FacingDirection.Left;
            player.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetFloat("y", 1);
            //anim.SetBool("walk", true);
            playerDir = FacingDirection.Backward;
            player.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetFloat("y", 1);
            //anim.SetBool("walk", true);
            playerDir = FacingDirection.Right;
            player.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
    }

    void Move3D()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float forwardBack = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float turning = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        float leaning = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        player.transform.Translate(Vector3.forward * forwardBack + Vector3.right * strafe, Space.Self);

        player.transform.Rotate(Vector3.up * turning);

        if (pivotPoint.transform.rotation.x < 30 && pivotPoint.transform.rotation.x > -30)
        {
            pivotPoint.transform.Rotate(Vector3.left * leaning);
        }
        //end of new code


        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10000, 0));
            jump = false;
        }

        if(strafe > 0)
        {
            anim.SetFloat("x", -1);
        }
        else if(strafe < 0)
        {
            anim.SetFloat("x", 1);
        }

        if (forwardBack != 0)
        {
            anim.SetFloat("y", 1);
            //anim.SetBool("walk", true);
        }
        else
        {
            anim.SetFloat("y", 0);
            //anim.SetBool("walk", false);
        }
    }

    void DustKickOff()
    {
        Instantiate(dust, player.transform.position, Quaternion.Inverse(player.transform.rotation));
        //Debug.Log("Kicking off Dust");
    }

    /*
    * Combat
    */

    



}
