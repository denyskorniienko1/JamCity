using UnityEngine;

public class Health : MonoBehaviour
{
    public FloatObservableVariableSO FloatObservableVariable;

    public EventSO OnDie;

    private bool isDead;

    public void Start()
    {
        FloatObservableVariable.OnGameRun();
    }

    public void Add(float value)
    {
        if (isDead) return;

        FloatObservableVariable.RuntimeValue += value;
    }

    public void Remove(float value)
    {
        if (isDead) return;

        FloatObservableVariable.RuntimeValue -= value;
        if(FloatObservableVariable.RuntimeValue < 0)
        {
            OnDie.Raise();
            isDead = true;
        }
    }
}