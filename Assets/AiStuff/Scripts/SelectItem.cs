using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;

public class SelectItem : MonoBehaviour
{

    float timeLeft = 4f;
    public bool Jump = false;



    // Use this for initialization
    void Start()
    {
        var ClickStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButton(0));


    }



    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Debug.Log("Jump");
            timeLeft = 4f;
        }
        if(timeLeft > 0 )
        {
            Debug.Log("Can not jump");
        }

    }
}

