using UnityEngine;

public class Health : MonoBehaviour
{
    public FloatObservableVariableSO FloatObservableVariable;

    public void Start()
    {
        FloatObservableVariable.OnGameRun();
    }
}