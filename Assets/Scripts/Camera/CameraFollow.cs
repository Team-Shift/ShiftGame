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
    private GameObject pivotPoint;
    public Vector3 Crosshair;

    // Use this for initialization
    void Start()
    {
        pivotPoint = GameObject.Find("PivotPoint");
    }

    // Update is called once per frame
    void Update()
    {
        //float playerPosX = player.transform.position.x;
        //float playerPosZ = player.transform.position.z;
        //cameraFollowPos = new Vector3(playerPosX, staticY, playerPosZ - 7.0f);
        //gameObject.transform.position = cameraFollowPos;
    }

    void LateUpdate()
    {
        if (player.GetComponent<Custom2DController>().CameraSwitch == true)
        {
            float offsetBack = 3;
            float turning = Input.GetAxis("Mouse X");

            transform.rotation = (pivotPoint.transform.rotation);
            //Vector3 pivotPos = pivotPoint.transform.position + offsetBack * -transform.forward;
            //pivotPos = new Vector3(pivotPos.x, pivotPos.y + 2.0f, pivotPos.z - 2.0f);
            transform.position = pivotPoint.transform.position + offsetBack * -transform.forward;
        }
    }
}
