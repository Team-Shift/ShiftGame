using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPosition;
    [HideInInspector]
    public Room.Direction targetDirection;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            TeleportEvent OnTeleport = new TeleportEvent(targetDirection, targetPosition);
            PostTeleportEvent PostTeleport = new PostTeleportEvent(targetDirection, targetPosition);

            GameEvents.Invoke(OnTeleport);
            GameEvents.Invoke(PostTeleport);
        }
    }
}
