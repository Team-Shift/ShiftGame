using UnityEngine;
using UnityEngine.Events;
using System.Collections;

//Everything required to make a player
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(HeartHealthUI))]
public class InputManager : MonoBehaviour
{
    public static InputManager playerInput;
    [HideInInspector]
    public UnityEvent OnShift = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnTurnScalarUp = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnTurnScalarDown = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMoveForward = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMoveBackward = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMoveLeft = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnMoveRight = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAttack = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnJump = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSwapItems = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnUseConsumable1 = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnUseConsumable2 = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAddGold = new UnityEvent();
    [HideInInspector]
    public GameObject player;
    [SerializeField]
    public float shiftCoolDownTime = 0.0f;
    [SerializeField]
    private float shiftTimer = 0.0f;
    private bool canShift = true;

    // Use this for initialization
    void Start()
    {
        shiftTimer = shiftCoolDownTime;
        player = gameObject;

        if(!playerInput)
        {
            playerInput = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(playerInput != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    //THIS IS THE ONLY PLACE WHERE ANY USER INPUT SHOULD BE TAKEN
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            //Do everything needed on shift and then set shift to false
            if (canShift)
            {
                OnShift.Invoke();
                canShift = !canShift;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            OnTurnScalarUp.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            OnTurnScalarDown.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJump.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnSwapItems.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnUseConsumable1.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnUseConsumable2.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.O) && Input.GetKeyDown(KeyCode.P))
        {
            OnAddGold.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnMoveForward.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnMoveLeft.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnMoveBackward.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnMoveRight.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnAttack.Invoke();
        }
    }

    void FixedUpdate()
    {
        //don't start counting down on the timer until the player has hit shift
        if (shiftTimer > 0 && !canShift)
        {
            shiftTimer--;
        }
        else if (shiftTimer <= 0)
        {
            shiftTimer = shiftCoolDownTime;
            canShift = !canShift;
        }
    }
}
