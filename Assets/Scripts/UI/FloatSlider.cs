using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatSlider : MonoBehaviour
{
    public Slider slider;

    public VariableSO<float> FloatVariable;

    public void Awake()
    {
        slider.maxValue = FloatVariable.MaxValue;
    }

    public void Redraw(float value)
    {
        if (slider == null)
            return;

        slider.value = value;
    }
}
