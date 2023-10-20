using UnityEngine;
using UnityEngine.Events;

public class OwnValueBasedHealth : HealthBase
{
    public UnityEvent OnDead;

    [SerializeField] private float _value;
    private bool isDead;

    public override void Add(float value)
    {
		if (isDead) return;

		_value += value;
	}

    public override void Remove(float value)
    {
        if (isDead) return;

        _value -= value;
        if(_value < 0)
        {
            isDead = true;
            OnDead?.Invoke();
        }
    }
}