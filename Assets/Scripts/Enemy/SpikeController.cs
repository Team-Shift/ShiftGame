using UnityEngine;
using System.Collections;

public class SpikeController : MonoBehaviour
{
    public GameObject player;
    private float posOffset;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
       // anim = gameObject.transform.parent.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        posOffset = gameObject.GetComponent<BoxCollider>().size.x / 2;
    }

    void FixedUpdate()
    {
        Vector3 spikePos = gameObject.transform.position;
        Vector3 playerPos = player.gameObject.transform.position;

        if (Mathf.Abs(playerPos.x - spikePos.x) < posOffset && Mathf.Abs(playerPos.z - spikePos.z) < posOffset && playerPos.y > spikePos.y)
        {
            //Activate animation
            Debug.Log("Player Is Hit");
                anim.SetTrigger("Hit");
        }
    }

    void OnTriggerEnter()
    {
        player.GetComponent<PlayerCombat>().DamageFallback(gameObject.transform.position);
    }
}
