
using UnityEngine.Events;
using UnityEngine;

public class EventListener<T> : MonoBehaviour
{
    public UnityEvent<T> Response;

    public EventSO<T> Event;

    public void OnEnable()
    {
        Event.AddListener(this);
    }

    public void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnRaised(T arg)
    {
        Response.Invoke(arg);
    }
}