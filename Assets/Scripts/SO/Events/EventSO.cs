using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "SO/Events/Event")]
public class EventSO : ScriptableObject
{
    private HashSet<EventListener> listeners = new HashSet<EventListener>();

    public void Raise()
    {
        foreach (EventListener listener in listeners)
        {
            listener.OnRaised();
        }
    }

    public void AddListener(EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            Debug.LogError($"Listener {listener.name} has been already subscribed on changes.");
            return;
        }

        listeners.Add(listener);
    }

    public void RemoveListener(EventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            Debug.LogError($"Listener {listener.name} is not subscribed on changes or has been already removed.");

            return;
        };

        listeners.Remove(listener);
    }
}

public class EventSO<T> : ScriptableObject
{
    private HashSet<EventListener<T>> listeners = new HashSet<EventListener<T>>();

    public void Raise(T arg)
    {
        foreach (EventListener<T> listener in listeners)
        {
            listener.OnRaised(arg);
        }
    }

    public void AddListener(EventListener<T> listener)
    {
        if (listeners.Contains(listener))
        {
            Debug.LogError($"Listener {listener.name} has been already subscribed on changes.");
            return;
        }

        listeners.Add(listener);
    }

    public void RemoveListener(EventListener<T> listener)
    {
        if (!listeners.Contains(listener))
        {
            Debug.LogError($"Listener {listener.name} is not subscribed on changes or has been already removed.");

            return;
        };

        listeners.Remove(listener);
    }
}