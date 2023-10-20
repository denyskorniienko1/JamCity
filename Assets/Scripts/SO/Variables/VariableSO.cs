using System;
using UnityEngine;

public class VariableSO<T> : ScriptableObject
{
	public T InitialValue;

    public bool HasMax;

    public T MaxValue;

	public virtual T RuntimeValue { get; set; }

    public void OnGameRun()
    {
        RuntimeValue = InitialValue;
    }
}