
using UnityEngine.Events;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public UnityEvent Response;

    public EventSO Event;

    public void OnEnable()
    {
        Event.AddListener(this);
    }

    public void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnRaised()
    {
        Response.Invoke();
    }
}

public class EventListener<T> : MonoBehaviour
{
    public UnityEvent<T> Response;

    public EventSO<T> Event;

    public void OnEnable()
    {
        Subscribe();
    }

    public void OnDisable()
    {
        Unsubscribe();
    }

    public void Subscribe()
    {
        Event.AddListener(this);
    }

    public void Unsubscribe()
    {
        Event.RemoveListener(this);
    }

    public void OnRaised(T arg)
    {
        Response.Invoke(arg);
    }
}