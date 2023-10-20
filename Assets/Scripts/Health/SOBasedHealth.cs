using UnityEngine;

public class SOBasedHealth : HealthBase
{
    public FloatObservableVariableSO FloatObservableVariable;

    public EventSO OnDie;

    private bool isDead;

    public void Start()
    {
        FloatObservableVariable.OnGameRun();
    }

    public override void Add(float value)
    {
        if (isDead) return;

        FloatObservableVariable.RuntimeValue += value;
    }

    public override void Remove(float value)
    {
        if (isDead) return;

        FloatObservableVariable.RuntimeValue -= value;
        if (FloatObservableVariable.RuntimeValue < 0)
        {
            OnDie.Raise();
            isDead = true;
        }
    }
}