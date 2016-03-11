using UnityEngine;
using System.Collections;

public class PlayerProjectialControl : MonoBehaviour
{
    public float lifeSpan = 0;


    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeSpan);
    }
}
