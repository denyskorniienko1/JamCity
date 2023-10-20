using UnityEngine;

public class ObservableVariable<T> : VariableSO<T>
{
	public EventSO<T> Event;

    public override T RuntimeValue { 
        
        get => base.RuntimeValue; 
        
        set => Set(value); 
    }

    private void Set(T value)
    {
        if (Event != null)
        {
            Event.Raise(value);

            base.RuntimeValue = value;
        }
    }
}