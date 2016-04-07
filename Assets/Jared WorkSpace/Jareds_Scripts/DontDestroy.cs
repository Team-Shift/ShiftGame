using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (gameObject.GetComponent<AudioSource>())
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
    }
}
