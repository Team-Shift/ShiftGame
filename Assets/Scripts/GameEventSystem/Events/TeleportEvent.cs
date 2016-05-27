using UnityEngine;
using System.Collections;

public class TeleportEvent : IGameEvent
{
    public Room.Direction Direction { get; private set; }

    public Vector3 TargetPosition { get; private set; }

    public TeleportEvent(Room.Direction direction, Vector3 targetPosition)
    {
        Direction = direction;
        TargetPosition = targetPosition;
    }

    public TeleportEvent()
    {
        
    }
}

public class PostTeleportEvent : IGameEvent
{
    public Room.Direction Direction { get; private set; }

    public Vector3 TargetPosition { get; private set; }

    public PostTeleportEvent(Room.Direction direction, Vector3 targetPosition)
    {
        Direction = direction;
        TargetPosition = targetPosition;
    }

    public PostTeleportEvent()
    {

    }

}