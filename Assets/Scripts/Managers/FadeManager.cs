using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{

    Color s_fade;                                       // to change alpha
    public Texture sprite;                           // black sprite
    SpriteRenderer spriteRend;                          // to get/set spriterenderer color
    Camera cam;                                         // camera   
    float startTime;                                    // for pingPonging fade to start at 0
    public float duration = 0.5f;                       // how long to fade

    void Start()
    {
        cam = Camera.main;
        //spriteRend = sprite;
    }

    void Update()
    {
        //startTime = Time.unscaledTime;
        //// disable fading after full fade duration
        //StartCoroutine("ShiftFade");
    }

    public void lerpAlpha()
    {
        // change alpha depending on time
        float lerp = Mathf.PingPong(Time.unscaledTime - startTime, duration) / duration;
        s_fade.a = lerp;
        spriteRend.color = s_fade;
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

        Time.timeScale = 1;
    }
}
