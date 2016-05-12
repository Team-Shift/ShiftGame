using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour {

    public Image img;

    void Start()
    {
        img.color = Color.Lerp(img.color, Color.black, Time.deltaTime * 10);
    }

    void OnTriggerEnter()
    {
        if (img.color.a < 0.5f)
        {
            this.transform.LoadScene(2);
        }
    }

}
