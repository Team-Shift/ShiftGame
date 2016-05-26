using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
	// Use this for initialization
	private void Start () {

    }
	
	// Update is called once per frame
    private void Update()
    {

    }

    //[HideInInspector]
    public Vector3 targetPosition;
    [HideInInspector]
    public Room.Direction targetDirection;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.position = targetPosition;

            Custom2DController player = col.gameObject.GetComponent<Custom2DController>();
            switch (targetDirection)
            {
                case Room.Direction.North:
                    player.playerMapPosition += new Vector2(0, 1);
                    break;
                case Room.Direction.East:
                    player.playerMapPosition += new Vector2(1, 0);
                    break;
                case Room.Direction.South:
                    player.playerMapPosition -= new Vector2(0, 1);
                    break;
                case Room.Direction.West:
                    player.playerMapPosition -= new Vector2(1, 0);
                    break;
            }
        }

        //ToDo Trigger OnEntrance of a Room
    }
}
