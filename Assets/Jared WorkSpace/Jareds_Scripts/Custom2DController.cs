using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Custom2DController : MonoBehaviour
{
    public GameObject player;
    public float turnSpeed = 180f;
    public float speed = 6.0f;
    public float gravity = 20.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private Rigidbody body;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);
    }

    void DamageFallback()
    {
        //Push the player back
        transform.Translate(new Vector3());
    }
}
