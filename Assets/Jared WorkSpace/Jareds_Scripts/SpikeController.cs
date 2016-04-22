using UnityEngine;
using System.Collections;

public class SpikeController : MonoBehaviour
{
    public Custom2DController player;
    private float posOffset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Custom2DController>();
        posOffset = gameObject.GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 spikePos = gameObject.transform.position;
        Vector3 playerPos = player.gameObject.transform.position;

        if (Mathf.Abs(playerPos.x - spikePos.x) < posOffset && Mathf.Abs(playerPos.z - spikePos.z) < posOffset && playerPos.y > spikePos.y)
        {
            if(player.CameraSwitch == false)
            {
                //Activate animation
                player.DamageFallback(gameObject.transform.position);
            }
        }
    }
}
