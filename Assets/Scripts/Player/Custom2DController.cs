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
    
    public float turnSpeed = 1f;
    public int turnScalar = 1;
    public float speed = 6.0f;
    [HideInInspector]
    private Vector3 moveDirection = Vector3.zero;
    //[HideInInspector]=
    private Animator anim;

    private float goalPitch;
    private float goalYaw;
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

    int count = 0;

    //ToDo Remove or replace map position
    public Vector2 playerMapPosition;

    // Use this for initialization
    void Start()
    {
        // Game Event Subscriptions
        GameEvents.Subscribe(HandleOnTeleportEvent, typeof(TeleportEvent));
        GameEvents.Subscribe(HandlePostTeleportEvent, typeof(PostTeleportEvent));

        //Input Manager Subscriptions
        InputManager.Instance.OnTurnScalarUp.AddListener(HandleOnScaleUpEvent);
        InputManager.Instance.OnTurnScalarDown.AddListener(HandleOnScaleDownEvent);
        InputManager.Instance.OnShift.AddListener(HandleOnShiftEvent);
        //InputManager.Instance.OnMoveForward.AddListener(HandleOnMoveForwardEvent);

        playerDir = FacingDirection.Forward;
        //currentHeld = CurrentItemType.None;
        anim = player.GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        //camShift = FindObjectOfType<Camera>();
        playerMapPosition = new Vector2(2,2);


        //Set target framerate to 60fps when running in editor
        #if UNITY_EDITOR
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        #endif

        if (gameObject.GetComponent<AudioSource>())
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnDestroy()
    {
        //GameEvents.UnsubscribeAll(HandleOnTeleportEvent);
        GameEvents.Unsubscribe(HandleOnTeleportEvent, typeof(TeleportEvent));
    }

    // Update is called once per frame
    void Update()
    {
        count++;

        if(transform.position.y <= -5)
        {
            StartCoroutine("PlayerDeath");
        }

        //if (transform.position.y <= -10)
        //{
        //    anim.SetFloat("DeathIndex", 1);
        //    anim.SetTrigger("Death");
        //    SceneManager.LoadScene("EmptyTown");
        //}

        //Movement
        if (InputManager.Instance.is2D)
        {
            Move2D();
        }
        else
        {
            Move3D();
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
    void Move2D(){
        //SO I CAN MOVE MY GODDAMN MOUSE IN 2D DON'T FUCKING COMMENT IT OUT FUCKERS IT DOESNT MAKE THE GAME ANY WORSE THEN IT ALREADY IS >:U 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        if (Input.GetAxis("Horizontal") != 0){
            anim.SetFloat("y", 1);

            if(Input.GetAxis("Horizontal") < 0){
                this.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
            }
            else{
                this.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            }
        }

        if(Input.GetAxis("Vertical") != 0){
            anim.SetFloat("y", 1);

            if (Input.GetAxis("Vertical") < 0){
                this.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
            else{
                this.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) { anim.SetFloat("y", 0); }

        /* Garbage Code*/

        //// ToDo Movement to Event Handlers
        //#region PhysicalMovementOfPlayer
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //if (moveDirection.x != 0 || moveDirection.z != 0)
        //{
        //    //Setting Y parameter to 1        Y Parameter 0 = Idle  1 = Walk
        //    anim.SetFloat("y", 1);
        //    //anim.SetBool("walk", true);
        //}
        //else if (moveDirection.x == 0 || moveDirection.z == 0)
        //{
        //    anim.SetFloat("y", 0);
        //    //anim.SetBool("walk", false);
        //}

        //transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);
        //#endregion

        //// Move Forward Moved to Handler
        //// ToDo Move the rest of movement if it works
        //#region AnimationOfPlayerMovement
        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    anim.SetFloat("y", 1);
        //    //anim.SetBool("walk", true);
        //    playerDir = FacingDirection.Left;
        //    player.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        //}
        //if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    anim.SetFloat("y", 1);
        //    //anim.SetBool("walk", true);
        //    playerDir = FacingDirection.Backward;
        //    player.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        //}
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    anim.SetFloat("y", 1);
        //    //anim.SetBool("walk", true);
        //    playerDir = FacingDirection.Right;
        //    player.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        //}
        //#endregion

    }

    void Move3D()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float forwardBack = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        goalYaw   += Input.GetAxis("Mouse X") * turnSpeed * turnScalar * Time.deltaTime;
        goalPitch -= Input.GetAxis("Mouse Y") * turnSpeed * turnScalar * Time.deltaTime;
        cameraPitch  = Mathf.Clamp(cameraPitch + 90.0f, 60, 120) - 90.0f;

		cameraYaw = goalYaw;
		cameraPitch = goalPitch;
        //cameraYaw   = Mathf.Lerp(cameraYaw  , goalYaw  , .2f);
        //cameraPitch = Mathf.Lerp(cameraPitch, goalPitch, .2f);


        player.transform.Translate(Vector3.forward * forwardBack + Vector3.right * strafe, Space.Self);

        
        transform.eulerAngles = new Vector3(cameraPitch, cameraYaw, 0);
        //end of new code


        if (Input.GetButton("Jump") && jump == true)
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10000, 0));
            jump = false;
        }
		//Debug.Log (strafe);
		if (strafe > 0) {
			anim.SetTrigger ("RightStrafe");
			anim.SetFloat("x", 1);
		} else if (strafe < 0) {
			anim.SetTrigger ("LeftStrafe");
			anim.SetFloat("x", -1);
		} else 
		{
			strafe = 0;
			anim.SetFloat ("x", 0);
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

    void HandleOnTeleportEvent(IGameEvent gameEvent)
    {
        TeleportEvent teleport = gameEvent as TeleportEvent;

        if (teleport != null)
        {
            // Move the player to portals target position
            transform.position = teleport.TargetPosition;
        }
        else
        {
            Debug.Log("Invalid Invoke OnTeleport");   
        }
    }

    void HandlePostTeleportEvent(IGameEvent gameEvent)
    {
        PostTeleportEvent teleport = gameEvent as PostTeleportEvent;

        if (teleport != null)
        {
            // Update Players Map Position After Teleporting
            switch (teleport.Direction)
            {
                case Room.Direction.North:
                    playerMapPosition += new Vector2(0, 1);
                    break;
                case Room.Direction.East:
                    playerMapPosition += new Vector2(1, 0);
                    break;
                case Room.Direction.South:
                    playerMapPosition -= new Vector2(0, 1);
                    break;
                case Room.Direction.West:
                    playerMapPosition -= new Vector2(1, 0);
                    break;
            }
        }
        else
        {
            Debug.Log("Invalid Invoke PostTeleport");
        }
    }


    // Input Handlers
    // Scale Camera Rotation Speed
    void HandleOnScaleUpEvent()
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

    void HandleOnScaleDownEvent()
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

    // Camera Shift
    void HandleOnShiftEvent()
    {
        if (!InputManager.Instance.is2D)
        {
            player.layer = LayerMask.NameToLayer("AvoidLight2D");
        }
        else
        {
            player.layer = LayerMask.NameToLayer("Default");
        }
    }

    
    //Why the fuck do we still have this
    //// ToDo Implement Back/Left/Right Events
    //void HandleOnMoveForwardEvent()
    //{
    //    if (InputManager.Instance.is2D)
    //    {
    //        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
    //        {
    //            anim.SetFloat("y", 1);
    //            //anim.SetBool("walk", true);
    //            playerDir = FacingDirection.Forward;
    //            player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("You're attempting to move in 3D forward");
    //    }
    //}

    IEnumerator PlayerDeath()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(50f);
        SceneManager.LoadScene("FinalTown");
    }
}