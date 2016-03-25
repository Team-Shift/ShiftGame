using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float staticY = 0;
    [HideInInspector]
    private Vector3 cameraFollowPos;

    private Custom2DController playerStatus;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerPosX = player.transform.position.x;
        float playerPosZ = player.transform.position.z;

        cameraFollowPos = new Vector3(playerPosX, staticY, playerPosZ - 7.0f);

        gameObject.transform.position = cameraFollowPos;
    }
}
