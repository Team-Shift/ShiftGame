using UnityEngine;
using System.Collections;

public class CameraShift : MonoBehaviour {

    // player to follow
    public GameObject player;
    private GameObject pivotPoint;

    // for fading in/out
    Color s_fade;                                       // to change alpha
    public GameObject sprite;                           // black sprite
    SpriteRenderer spriteRend;                          // to get/set spriterenderer color
    Camera cam;                                         // camera    
    bool canFade;                                       // used to fade in update
    public bool camActive;                              // false is ortho and true is persp
    float startTime;                                    // for pingPonging fade to start at 0
    public float duration = 0.5f;                       // how long to fade

    public bool canShift;                               // if player unlocks shift ability

    // get players pos
    float playerPosX;
    float playerPosZ;
    float playerPosY;

    void Start()
    {
        pivotPoint = GameObject.Find("PivotPoint");

        // changes if unlocked in game
        canShift = true;
        camActive = false;  // start in ortho
        // get camera
        cam = gameObject.GetComponent<Camera>();
        // get rend to change
        spriteRend = sprite.GetComponent<SpriteRenderer>();
        canFade = false;    // no fading to start
    }

	void Update () {

        // get players pos
        playerPosX = player.transform.position.x;
        playerPosZ = player.transform.position.z;
        playerPosY = player.transform.position.y;


        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) && canShift)
        {
            canFade = true;
            startTime = Time.unscaledTime;
            // change camera at half duration
            StartCoroutine("CameraChange");
            // disable fading after full fade duration
            StartCoroutine("ShiftFade");

            // perspective cam position
            if (camActive)
            {
                Vector3 v = new Vector3(playerPosX, playerPosY + 2.0f, playerPosZ - 2.0f);
                gameObject.transform.position = v;
                gameObject.transform.rotation = Quaternion.Euler(20.0f, 0, 0);
                cam.orthographic = false;
            }
            // ortho camera position
            else
            {
                Vector3 v = new Vector3(playerPosX, playerPosY + 10.0f, playerPosZ - 6.0f);
                gameObject.transform.position = v;
                gameObject.transform.rotation = Quaternion.Euler(50.0f, 0, 0);
                cam.orthographic = true;
            }
        }

        // start fading in and out
        if (canFade)
            lerpAlpha();
        
    }


    /*
    ||===================================================================================||
    *       DON'T TOUCH LATE UPDATE                                                       *
    *       THIS ALLOWS TO PLAYER AND CAMERA TO ROATE THE SAME                            *
    ||===================================================================================||
    */
    void LateUpdate()
    {
        if (camActive == true)
        {
            float offsetBack = 3;
            float turning = Input.GetAxis("Mouse X");

            transform.rotation = (pivotPoint.transform.rotation);
            transform.position = pivotPoint.transform.position + offsetBack * -transform.forward;
        }
    }
    
    void ChangeCamera()
    {
        // ortho -> persp
        if (camActive) camActive = false;
        // persp -> ortho
        else camActive = true;
    }

    void lerpAlpha()
    {
        // change alpha depending on time
        float lerp = Mathf.PingPong(Time.unscaledTime - startTime, duration)/duration;
        s_fade.a = lerp;
        spriteRend.color = s_fade;
    }

    IEnumerator CameraChange()
    {
        float timeToWait = duration;
        while (timeToWait >= 0f)
        {
            timeToWait -= Time.unscaledDeltaTime;
            yield return null;
        }
        ChangeCamera();
    }

    IEnumerator ShiftFade()
    {
        Time.timeScale = 0;
        // stop changing alpha after duration*2
        float timeToWait = duration + duration;
        while (timeToWait >= 0f)
        {
            timeToWait -= Time.unscaledDeltaTime;
            yield return null;
            //yield return new WaitForSeconds(duration + duration);
        }
        
        canFade = false;    
        Time.timeScale = 1;
    }
}
