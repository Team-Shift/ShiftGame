using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 0.0f;
    [SerializeField]
    public float turnSpeed = 0.0f;
    [SerializeField]
    public int turnScalar = 1;
    [HideInInspector]
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private float cameraPitch;
    [SerializeField]
    private float cameraYaw;
    [SerializeField]
    public float jumpForce = 10;
    private bool jump = true;
    [SerializeField]
    public float jumpTimeLeft = 1f;
    [HideInInspector]
    private bool is2D = true;

    [HideInInspector]
    private GameObject player;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        //player = InputManager.playerInput.player;
        player = gameObject;
        Debug.Log(player.name);
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        InputManager.playerInput.OnShift.AddListener(HandleOnShiftEvent);
        InputManager.playerInput.OnTurnScalarUp.AddListener(HandleOnPageUpEvent);
        InputManager.playerInput.OnTurnScalarDown.AddListener(HandleOnPageDownEvent);
        InputManager.playerInput.OnMoveForward.AddListener(HandleOnMoveForward);
        InputManager.playerInput.OnMoveBackward.AddListener(HandleOnMoveBackward);
        InputManager.playerInput.OnMoveLeft.AddListener(HandleOnMoveLeft);
        InputManager.playerInput.OnMoveRight.AddListener(HandleOnMoveRight);
        InputManager.playerInput.OnJump.AddListener(HandleOnJumpEvent);

        //Set target framerate to 60fps when running in editor
        #if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        //Update Jumping
        if (jump == false)
        {
            jumpTimeLeft = jumpTimeLeft - Time.deltaTime;
        }
        if (jumpTimeLeft <= 0)
        {
            jumpTimeLeft = 1f;
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (is2D)
        {
            Move2D();
        }
        else
        {
            Move3D();
        }
    }

    //Subscriber Event Handlers
    private void HandleOnShiftEvent()
    {
        is2D = !is2D;
    }
    private void HandleOnPageUpEvent()
    {
        UpdateTurnScalar(1);
    }
    private void HandleOnPageDownEvent()
    {
        UpdateTurnScalar(-1);
    }
    private void HandleOnJumpEvent()
    {
        if (jump && !is2D)
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce * 10, 0));
            jump = false;
        }
    }
    private void HandleOnMoveForward()
    {
        if (is2D)
        {
            anim.SetFloat("y", 1);
            player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }
    private void HandleOnMoveBackward()
    {
        if (is2D)
        {
            anim.SetFloat("y", 1);
            player.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
    }
    private void HandleOnMoveLeft()
    {
        if (is2D)
        {
            anim.SetFloat("y", 1);
            player.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        }
    }
    private void HandleOnMoveRight()
    {
        if (is2D)
        {
            anim.SetFloat("y", 1);
            player.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
    }


    private void UpdateTurnScalar(int changeAmount)
    {
        if (turnScalar > 12)
        {
            turnScalar = 12;
        }
        else if(turnScalar < -12)
        {
            turnScalar = -12;
        }
        else
        {
            turnScalar += changeAmount;
        }
    }

    void Move2D()
    {
        Debug.Log("Moving in 2D");

        // Pre movement setup code
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        //If we're in the editor don't hide the cursor
        #if UNITY_EDITOR
        Cursor.visible = true;
        #endif

        player.layer = LayerMask.NameToLayer("AvoidLight2D");

        //Movement Code
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            //Setting Y parameter to 1        Y Parameter 0 = Idle  1 = Walk
            anim.SetFloat("y", 1);
        }
        else if (moveDirection.x == 0 || moveDirection.z == 0)
        {
            anim.SetFloat("y", 0);
        }

        transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
    }

    void Move3D()
    {
        Debug.Log("Moving in 3D");

        // Pre movement setup code
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //If we're in the editor don't hide the cursor or lock the mouse
        #if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        #endif

        player.layer = LayerMask.NameToLayer("Default");

        float forwardBack = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        cameraYaw += Input.GetAxis("Mouse X") * turnSpeed * turnScalar * Time.deltaTime;
        cameraPitch -= Input.GetAxis("Mouse Y") * turnSpeed * turnScalar * Time.deltaTime;

        //Movement Code
        player.transform.Translate(Vector3.forward * forwardBack + Vector3.right * strafe, Space.Self);

        cameraPitch = Mathf.Clamp(cameraPitch + 90.0f, 60, 120) - 90.0f;
        transform.eulerAngles = new Vector3(cameraPitch, cameraYaw, 0);

        //Animation visual code
        if (strafe > 0)
        {
            anim.SetTrigger("LeftStrafe");
        }
        else if (strafe < 0)
        {
            anim.SetTrigger("RightStrafe");
        }

        if (strafe == 0)
        {
            anim.SetFloat("x", 0);
        }

        if (forwardBack != 0)
        {
            anim.SetFloat("y", 1);
        }
        else
        {
            anim.SetFloat("y", 0);
        }
    }
}
