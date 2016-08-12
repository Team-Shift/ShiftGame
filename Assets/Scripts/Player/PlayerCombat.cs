using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour {
    
    public GameObject rangedTemp;
    public GameObject meleeWeapon;
    public GameObject sword;
	TownManager townMan;

    private AudioSource playerSound;
    public AudioClip meleeSound;
    public AudioClip bowSound;
    public AudioClip hurtSound;

    public float pushBackForce = 750;
    private bool shot = true;
    public float shotTimeLeft = 1f;
    public bool melee = true;
    public float meleeTimeLeft = .4f;
    public float worldLimit = 0;

    //[HideInInspector]
    public int Health = 3;
    public int Attack;
    bool isDead;

    private Animator anim;
    private Inventory inv;

    HealthUI hearts;

    // Use this for initialization
    void Start () {
		//townMan = GameObject.Find ("TownCutSceneManager").GetComponent<TownManager>();
        playerSound = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        hearts = gameObject.GetComponent<HealthUI>();
        Health = 3;
        Attack = 1;
        inv = gameObject.GetComponent<Inventory>();
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Combat & Interation
        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("attacking");
            //Debug.Log(inv);
            if (inv.invItems[0].item.itemName == "Bow" || inv.invItems[0].item.itemName == "ChickenBow")
            {
                RangedAttack();
                //Debug.Log("bow");
            }
            else
            {
                //melee = !melee;
                MeleeAttack();
                //Debug.Log("meelee");
            }
            
        }
        //else if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    RangedAttack();
        //}

        if (shot == false)
        {
            shotTimeLeft = shotTimeLeft - Time.deltaTime;
        }

        if (shotTimeLeft <= 0)
        {
            shotTimeLeft = 1f;
            shot = true;
        }

        // putting melee on a timer
        if (melee == false)
        {
            meleeTimeLeft = meleeTimeLeft - Time.deltaTime;
        }

        if (meleeTimeLeft <= 0)
        {
            meleeTimeLeft = .475f;
            melee = true;
        }

        if (gameObject.transform.position.y <= worldLimit)
        {
            Health = 0;
        }

        if (Health <= 0)
        {
            anim.SetTrigger("Death");
            SendToTown();
            //Invoke("SendToTown", 1);
        }

        if (gameObject.transform.position.y <= -10)
        {
            //anim.SetFloat("DeathIndex", 1);
            anim.SetTrigger("Death");
            SendToTown();
            //SceneManager.LoadScene("EmptyTown");
        }
    }

    public void DeathStuff()
    {
        Debug.Log("DeathStuff");
        Instantiate(Resources.Load("DeathText"));
        
        Invoke("SendToTown", 2);
    }

    //ToDo Please Remove Destruction of Everything
    void SendToTown()
    {
		//townMan.IncreaseDeathCount ();
        foreach (var everything in FindObjectsOfType<GameObject>())
        {
            Destroy(everything);
        }
        SceneManager.LoadScene("FinalTown");
    }

    public void DamageFallback(Vector3 damageSource)
    {
        Health--;
        hearts.DamageHeart();

        playerSound.PlayOneShot(hurtSound);
        StartCoroutine(ChangeColor(1, 0.1f, 0.1f, 1, 0.5f));

        if (gameObject.transform.position.z < damageSource.z)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.forward * pushBackForce);
        }
        else if (gameObject.transform.position.z > damageSource.z)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * pushBackForce);
        }
        if (gameObject.transform.position.x < damageSource.x)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.right * pushBackForce);
        }
        else if (gameObject.transform.position.x > damageSource.x)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * pushBackForce);
        }

        StartCoroutine(StopForce());

    }


    public void AttackPushForward()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 10000);
    }

    void MeleeAttack()
    {
        if (melee == true)
        {
            playerSound.PlayOneShot(meleeSound);
            anim.SetTrigger("Attack");
            anim.SetFloat("WeaponIndex",0);
            //anim.SetTrigger("sword_attack");
            melee = false;
        }
    }

    void RangedAttack()
    {
        if (shot == true)
        {
            //Triggering Attack State
            anim.SetTrigger("Attack");

            //Setting Weapon Index for Attack
            anim.SetFloat("WeaponIndex", 1);
            //anim.SetTrigger("bow_attack");
        }

        Custom2DController.FacingDirection playerDir = gameObject.GetComponent<Custom2DController>().playerDir;
        if (InputManager.Instance.is2D)
        {
            if (playerDir == Custom2DController.FacingDirection.Forward && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + .3f), gameObject.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
            else if (playerDir == Custom2DController.FacingDirection.Backward && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - .3f), gameObject.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
            else if (playerDir == Custom2DController.FacingDirection.Left && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(gameObject.transform.position.x - .3f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }
            else if (playerDir == Custom2DController.FacingDirection.Right && shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, new Vector3(gameObject.transform.position.x + .3f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
                projectial.GetComponent<Rigidbody>().AddForce(transform.forward * 2000 * Time.deltaTime);
            }

        }

        else
        {
            if (shot == true)
            {
                playerSound.PlayOneShot(bowSound);
                GameObject projectial = Instantiate(rangedTemp, gameObject.transform.position + gameObject.transform.forward, gameObject.transform.rotation) as GameObject;
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

    public void ModifyHealth(int amount)
    {
        Health += amount;
    }

    public void ModifyAttack(int amount)
    {
        Attack = amount;
    }
}
