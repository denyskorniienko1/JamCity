using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public UnityEvent<Collision> OnCollisionDetected;

    public void OnCollisionEnter(Collision collision)
    {
        OnCollisionDetected.Invoke(collision);
        Debug.Log(collision.gameObject.name);
    }
}