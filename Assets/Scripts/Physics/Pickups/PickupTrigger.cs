using UnityEngine;
using UnityEngine.Events;

public abstract class PickupTrigger<T> : MonoBehaviour
{
    public VariableSO<T> Source;
    public VariableSO<T> ValueToAdd;
    public UnityEvent OnTriggered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Apply();
            OnTriggered?.Invoke();
        }
    }

    protected abstract void Apply();
}