using UnityEngine;
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
    public bool is2D = true;
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
    void Update()
    {
        //THIS SHOULD BE THE ONLY PLACE WHERE SHIFT INPUT IS TAKEN
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            //Do everything needed on shift and then set shift to false
            if (canShift)
            {
                OnShift();
                canShift = !canShift;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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

    private void OnShift()
    {
        //TODO:
        //Call all things that are supposed to happen on shift
        is2D = !is2D;
    }
}
