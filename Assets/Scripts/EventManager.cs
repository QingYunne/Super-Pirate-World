using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, Action<object>> eventTable = new();

    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (!eventTable.ContainsKey(eventName))
            eventTable[eventName] = listener;
        else
            eventTable[eventName] += listener;
    }

    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] -= listener;
            if (eventTable[eventName] == null)
                eventTable.Remove(eventName);
        }
    }

    public static void Trigger(string eventName, object param = null)
    {
        if (eventTable.ContainsKey(eventName))
            eventTable[eventName]?.Invoke(param);
    }
}
