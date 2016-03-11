using UnityEngine;
using System.Collections;

public class CameraShift : MonoBehaviour {
    // need to attach dif cameras in scene
    // or need to tag persp camera with tag in scene
    public GameObject ortho;
    public GameObject persp;

    // for fading in/out
    Color s_fadePersp, s_fadeOrtho;                     // to change alpha
    public GameObject spriteP, spriteO;
    SpriteRenderer spritePersp, spriteOrtho;            // to get/set spriterenderer color
    bool canFade;                                       // used to fade in update
    float startTime;                                    // for pingPonging fade to start at 0
    public float duration = 0.5f;                       // how long to fade

    public bool canShift;                               // if player unlocks shift ability

    void Start()
    {
        // changes if unlocked in game
        canShift = true;

        // get spriterenderer
        spritePersp = spriteP.GetComponent<SpriteRenderer>();
        spriteOrtho = spriteO.GetComponent<SpriteRenderer>();
        canFade = false;
        
        // get cameras if not found
        if(ortho == null)
            ortho = GameObject.FindGameObjectWithTag("MainCamera");
        if(persp == null)
            persp = GameObject.FindGameObjectWithTag("Perspective Camera");
    }

	void Update () {
	    if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) && canShift)
        {
            canFade = true;
            startTime = Time.unscaledTime;
            // change camera at half duration
            StartCoroutine("CameraChange");
            // disable fading after full fade duration
            StartCoroutine("ShiftFade");
        }
        // start fading in and out
        if(canFade)
            lerpAlpha();
        
    }
    
    void ChangeCamera()
    {
        if (ortho.activeInHierarchy == true)
        {
            ortho.SetActive(false);
            spriteO.SetActive(false);
            persp.SetActive(true);
            spriteP.SetActive(true);
        }
        else
        {
            ortho.SetActive(true);
            spriteO.SetActive(true);
            persp.SetActive(false);
            spriteP.SetActive(false);
        }
    }

    void lerpAlpha()
    {
        // change alpha depending on time
        float lerp = Mathf.PingPong(Time.unscaledTime - startTime, duration)/duration;
            s_fadePersp.a = lerp;
            spritePersp.color = s_fadePersp;
            s_fadeOrtho.a = lerp;
            spriteOrtho.color = s_fadeOrtho;
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
