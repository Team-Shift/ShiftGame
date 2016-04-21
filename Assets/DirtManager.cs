using UnityEngine;
using System.Collections;

public class DirtManager : MonoBehaviour
{
    private ParticleSystem ps;

    float lifeTime;

    // Use this for initialization
    void Start()
    {
        lifeTime = 0;
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > 1)
        {
            Destroy(gameObject);
            Debug.Log("die");
        }
    }

}
