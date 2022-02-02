using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevPanelSlider : DevPanelGUI
{
    public TextMeshProUGUI sliderTitle;
    public TextMeshProUGUI sliderValue;
    public Slider slider;

    [Header("Debug")]
    public MonoBehaviour targetReference;
    public string targetValue;

    public void Start()
    {
        float sourceValue = (float)targetReference.GetType().GetProperty(targetValue, typeof(float)).GetValue(targetReference, null);
        slider.value = sourceValue;
        UpdateSliderValueText(sourceValue);

        slider.onValueChanged.AddListener(newValue =>
        {
            targetReference.GetType().GetProperty(targetValue).SetValue(targetReference, newValue);
            float currentValue = (float)targetReference.GetType().GetProperty(targetValue).GetValue(targetReference, null);
            UpdateSliderValueText(currentValue);
        });
    }

    public void SetSliderTitle(string text)
    {
        
        sliderTitle.text = Capitalize(text);
    }

    public void SetReferences(MonoBehaviour reference, string targetValue)
    {
        this.targetValue = targetValue;
        targetReference = reference;
    }

    public void UpdateSliderValueText(float number)
    {
        sliderValue.text = number.ToString("0.00");
    }
}
