using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public abstract void Add(float value);
    public abstract void Remove(float value);
}