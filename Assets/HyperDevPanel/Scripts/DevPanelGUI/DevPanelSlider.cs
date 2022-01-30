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
        float sourceValue = (float)targetReference.GetType().GetProperty(targetValue).GetValue(targetValue, null);
        slider.value = sourceValue;
        UpdateSliderValueText(sourceValue);

        slider.onValueChanged.AddListener(newValue =>
        {
            targetReference.GetType().GetProperty(targetValue).SetValue(newValue, null);
            float currentValue = (float)targetReference.GetType().GetProperty(targetValue).GetValue(targetValue, null);
            UpdateSliderValueText(currentValue);
        });
    }

    public void SetSliderTitle(string text)
    {
        sliderTitle.text = text;
    }

    public void SetReferences(MonoBehaviour reference, string targetValue)
    {
        this.targetValue = targetValue;
        targetReference = reference;
    }

    public void UpdateSliderValueText(float number)
    {
        sliderValue.text = number.ToString(".00");
    }
}
