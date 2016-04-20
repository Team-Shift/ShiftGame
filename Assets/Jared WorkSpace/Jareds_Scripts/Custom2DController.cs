using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Custom2DController : MonoBehaviour
{
    public GameObject player;
    public GameObject rangedTemp;
    public GameObject meleeWeapon;
    public GameObject sword;
    private DimensionalSwitchManager manager;
    public float turnSpeed = 180f;
    public float speed = 6.0f;
    [HideInInspector]
    private Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public int Health = 3;
    [HideInInspector]
    public bool CameraSwitch = false;
    private Animator anim;

    private AudioSource playerSound;
    public AudioClip meleeSound;
    public AudioClip bowSound;
    public AudioClip hurtSound;

    public float pushBackForce = 750;
    public float pushUpForce = 10;
    private bool jump = true;
    public float jumpTimeLeft = 1f;
    private bool shot = true;
    public float shotTimeLeft = 1f;
    public bool melee = true;
    public float meleeTimeLeft = 1f;
    
    //Movement
    public enum FacingDirection { Forward, Backward, Left, Right };
    public FacingDirection playerDir;

    public GameObject dust;

    //Combat
     

    //UI
    public GUITexture HeartFillTexture;
    public GUITexture HeartContainerTexture;
    private List<GUITexture> HeartFillList = new List<GUITexture>();
    private List<GUITexture> HeartContainerList = new List<GUITexture>();
    int AmountOfHeartContainer;
    public float XOffset = 0.14f;
    public float YOffset = 0.92f;

    MenuManager sceneShit;


    // Use this for initialization
    void Start()
    {
        playerDir = FacingDirection.Forward;
//        currentHeld = CurrentItemType.None;
        manager = GameObject.FindObjectOfType<DimensionalSwitchManager>();
        anim = player.GetComponent<Animator>();
        CameraSwitch = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;

        playerSound = player.GetComponent<AudioSource>();
        sceneShit = FindObjectOfType<MenuManager>();

        // UI STUFF
        AmountOfHeartContainer = Health;
        SpawnHeart(Health);
        SpawnHeartContainer(AmountOfHeartContainer);

    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.y <= 0)
        {
            sceneShit.TownScene();
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

        //Combat
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            melee = !melee;
            MeleeAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RangedAttack();
        }

        //Shift
        if(Input.GetKeyDown(KeyCode.LeftShift))
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

        if (shot == false)
        {
            shotTimeLeft = shotTimeLeft - Time.deltaTime;
        }

        if (shotTimeLeft <= 0)
        {
            shotTimeLeft = 1f;
            shot = true;
        }

        if(Health <= 0)
        {
            this.transform.LoadScene(1);
        }


    }



    /*
    * Movement
    */
    void Move2D()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
            player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("walk", true);
            playerDir = FacingDirection.Left;
            player.transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("walk", true);
            playerDir = FacingDirection.Backward;
            player.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("walk", true);
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
        float turning = Input.GetAxis("Mouse X");

        player.transform.Translate(Vector3.forward * forwardBack + Vector3.right * strafe, Space.Self);

        player.transform.Rotate(Vector3.up * turning * turnSpeed * Time.deltaTime);
        //end of new code


        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10000, 0));
            jump = false;
        }

        if (forwardBack != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    void DustKickOff()
    {
        Instantiate(dust, player.transform.position, Quaternion.Inverse(player.transform.rotation));
        Debug.Log("Kicking off Dust");
    }

    /*
    * Combat
    */

    public void DamageFallback(Vector3 damageSource)
    {
        Health--;
        DamageHeart();

        playerSound.PlayOneShot(hurtSound);
        StartCoroutine(ChangeColor(1, 0.1f, 0.1f, 1, 0.5f));

        if (player.transform.position.z < damageSource.z)
        {
            player.GetComponent<Rigidbody>().AddForce(-Vector3.forward * pushBackForce);
        }
        else if (player.transform.position.z > damageSource.z)
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.forward * pushBackForce);
        }
        if (player.transform.position.x < damageSource.x)
        {
            player.GetComponent<Rigidbody>().AddForce(-Vector3.right * pushBackForce);
        }
        else if (player.transform.position.x > damageSource.x)
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.right * pushBackForce);
        }

        StartCoroutine(StopForce());

    }

    public void AttackPushForward()
    {
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward * 10000);
    }

    void MeleeAttack()
    {
        if(melee == true)
        {
            playerSound.PlayOneShot(meleeSound);
            anim.SetTrigger("sword_attack");
            melee = false;
        }
    }

    void RangedAttack()
    {
        if (shot == true)
        {
            anim.SetTrigger("bow_attack");
        }

        if (CameraSwitch == false)
        {
            if (playerDir == FacingDirection.Forward && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.0f), player.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
            else if (playerDir == FacingDirection.Backward && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 1.0f), player.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
            else if (playerDir == FacingDirection.Left && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x - 1.0f, player.transform.position.y, player.transform.position.z), player.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
            else if (playerDir == FacingDirection.Right && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(player.transform.position.x + 1.0f, player.transform.position.y, player.transform.position.z), player.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }

        }

        else
        {
            if (shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, player.transform.position + player.transform.forward, player.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
        }

        shot = false;
    }

    /*
    * Ienumerator Functions
    * (Used for WaitForSeconds function)
    */

    IEnumerator ChangeColor(float r, float g, float b, float a, float timeToWait)
    {
        Transform[] m = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform om in m)
        {
            if (om.GetComponent<Renderer>())
                om.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        }

        yield return new WaitForSeconds(timeToWait);

        foreach (Transform om in m)
        {
            if (om.GetComponent<Renderer>())
                om.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator StopForce()
    {
        float waitTime = 1f;

        yield return new WaitForSeconds(waitTime);

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    /*
    UI Stuffs
    */

    void SpawnHeart(int HeartAmount)
    {
        for (int i = 0; i < Health; i++)
        {
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }

    void SpawnHeartContainer(int HeartAmount)
    {

        for (int i = 0; i < Health; i++)
        {
            HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector2(i * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }


    public void AddHeartContainer()
    {
        AmountOfHeartContainer++;
        int HeartContainerIndex = HeartContainerList.Count;
        HeartContainerList.Add(((GUITexture)Instantiate(HeartContainerTexture, new Vector3(HeartContainerIndex * XOffset + .07f, YOffset, -1), Quaternion.identity)));
    }

    public void HealHeart()
    {
        if (HeartFillList.Count < HeartContainerList.Count)
        {
            Health++;
            int HeartFillIndex = HeartFillList.Count;
            HeartFillList.Add(((GUITexture)Instantiate(HeartFillTexture, new Vector2(HeartFillIndex * XOffset + .07f, YOffset), Quaternion.identity)));
        }
    }

    public void DamageHeart()
    {
        Destroy(HeartFillList[HeartFillList.Count - 1].gameObject);
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }

    public void RemoveHeartContainer()
    {
        AmountOfHeartContainer--;
        Destroy(HeartContainerList[HeartContainerList.Count - 1].gameObject);
        HeartFillList.RemoveAt(HeartFillList.Count - 1);
    }


}
