using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Custom2DController : MonoBehaviour
{
    public GameObject player;
    
    public float turnSpeed = 180f;
    public int turnScalar;
    public float speed = 6.0f;
    [HideInInspector]
    private Vector3 moveDirection = Vector3.zero;
    //[HideInInspector]
    public bool CameraSwitch = false;
    private Animator anim;
    private float cameraPitch;
    private float cameraYaw;

    public float pushUpForce = 10;
    private bool jump = true;
    public float jumpTimeLeft = 1f;
    
    //Movement
    public enum FacingDirection { Forward, Backward, Left, Right };
    public FacingDirection playerDir;

    public GameObject dust;
    //public Camera camShift;
    //Combat

    int count;

    // Use this for initialization
    void Start()
    {
        count = 0;
        turnScalar = 1;
        playerDir = FacingDirection.Forward;
        //currentHeld = CurrentItemType.None;
        anim = player.GetComponent<Animator>();
        CameraSwitch = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        //camShift = FindObjectOfType<Camera>();

        //Set target framerate to 60fps when running in editor
        #if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        count++;

        // ToDo Remove Debug for beta
        if (Input.GetKeyUp(KeyCode.PageUp))
        {
            if (turnScalar >= 12)
            {
                turnScalar = 12;
            }
            else
            {
                turnScalar++;
            }
        }

        if (Input.GetKeyUp(KeyCode.PageDown))
        {
            if (turnScalar <= 1)
            {
                turnScalar = 1;
            }
            else
            {
                turnScalar--;
            }
        }

        if (player.transform.position.y <= -10)
        {
            anim.SetFloat("DeathIndex", 1);
            anim.SetTrigger("Death");
            SceneManager.LoadScene("EmptyTown");
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

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            if (count > 75)
            {
                //Debug.Log("switching");
                CameraSwitch = !CameraSwitch;
                //manager.Shift();
                if (CameraSwitch == false)
                {
                    player.layer = LayerMask.NameToLayer("AvoidLight2D");
                }
                else
                {
                    player.layer = LayerMask.NameToLayer("Default");
                }
                count = 0;
            }
        }



        if (Input.GetKeyDown(KeyCode.Escape))
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
        cameraYaw += Input.GetAxis("Mouse X") * turnSpeed * turnScalar * Time.deltaTime;
        cameraPitch -= Input.GetAxis("Mouse Y") * turnSpeed * turnScalar * Time.deltaTime;

        player.transform.Translate(Vector3.forward * forwardBack + Vector3.right * strafe, Space.Self);

        cameraPitch = Mathf.Clamp(cameraPitch + 90.0f, 60, 120) - 90.0f;
        transform.eulerAngles = new Vector3(cameraPitch, cameraYaw, 0);
        //end of new code


        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10000, 0));
            jump = false;
        }

        if(strafe > 0)
        {
            anim.SetTrigger("LeftStrafe");
            //anim.SetFloat("x", -1);
        }
        else if(strafe < 0)
        {
            anim.SetTrigger("RightStrafe");
            //anim.SetFloat("x", 1);
        }

        if(strafe == 0)
        {
            anim.SetFloat("x", 0);
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
    }

    



}
