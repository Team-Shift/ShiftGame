using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class GameEvents {

    private static Dictionary<Type, Action<IGameEvent>> m_eventSubscribers;

    static GameEvents()
    {
        m_eventSubscribers = new Dictionary<Type, Action<IGameEvent>>();
    }

    public static void Subscribe( Action<IGameEvent> methodTarget, params Type[] eventTypes ) {
        if( eventTypes == null ) { throw new ArgumentNullException( "eventTypes" ); }
        if( methodTarget == null ) { throw new ArgumentNullException( "methodTarget" ); }

        for (int index = 0; index < eventTypes.Length; index++)
        {
            if (!typeof(IGameEvent).IsAssignableFrom(eventTypes[index]))
            {
                throw new ArgumentException(
                    string.Format("eventTypes[{0}] (Type: {1}) is not an IGameEvent",
                                    index,
                                    eventTypes[index]),
                    "eventTypes");
            }

            // Make sure the element exists to subscribe to
            if (!m_eventSubscribers.ContainsKey(eventTypes[index]))
            {
                m_eventSubscribers.Add(eventTypes[index], null);
            }

            m_eventSubscribers[eventTypes[index]] += methodTarget;
        }
    }

    public static void UnsubscribeAll( Action<IGameEvent> methodTarget ) {
        if( methodTarget == null ) { throw new ArgumentNullException( "methodTarget" ); }

        Type[] keys = m_eventSubscribers.Keys.ToArray();

        foreach (Type type in keys)
        {
            m_eventSubscribers[type] -= methodTarget;
        }
    }

    public static void Unsubscribe( Action<IGameEvent> methodTarget, params Type[] eventTypes ) {
        if( eventTypes == null ) { throw new ArgumentNullException( "eventTypes" ); }
        if( methodTarget == null ) { throw new ArgumentNullException( "methodTarget" ); }

        for (int index = 0; index < eventTypes.Length; index++)
        {
            if (!typeof(IGameEvent).IsAssignableFrom(eventTypes[index]))
            {
                throw new ArgumentException(
                    string.Format("eventTypes[{0}] (Type: {1}) is not an IGameEvent",
                                    index,
                                    eventTypes[index]),
                    "eventTypes");
            }

            // If the element doesn't exist we don;t need to bother trying to unsubscribe
            if (m_eventSubscribers.ContainsKey(eventTypes[index]))
            {
                m_eventSubscribers[eventTypes[index]] -= methodTarget;
            }
        }
    }

    public static void Invoke( IGameEvent gameEvent ) {
        Type eventType = gameEvent.GetType();
        if( m_eventSubscribers.ContainsKey( eventType ) ) {
            m_eventSubscribers[eventType].Invoke( gameEvent );
        }
        else {
            Debug.LogFormat( "#{0}# Skipping invoke (Type: {1}), it has no subscribers.",
                                typeof( GameEvents ).Name,
                                eventType );
        }
    }

}
