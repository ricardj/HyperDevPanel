using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DevPanelToogle : DevPanelGUI
{

    public TextMeshProUGUI toogleText;
    public Toggle toggle;

    [Header("Debug")]
    public MonoBehaviour targetReference;
    public string targetValue;

    public void Start()
    {
        bool sourceValue = (bool)targetReference.GetType().GetProperty(targetValue).GetValue(targetReference, null);
        toggle.isOn = sourceValue;


        toggle.onValueChanged.AddListener(newValue =>
        {
            toggle.isOn = newValue;
            targetReference.GetType().GetProperty(targetValue).SetValue(targetReference, newValue);
            //bool currentValue = (bool)targetReference.GetType().GetProperty(targetValue).GetValue(targetValue, null);

        });
    }

    internal void SetToogleName(string text)
    {
        toogleText.text = Capitalize(text);
    }

    internal void SetReferences(MonoBehaviour monoBehaviour, string propertyName)
    {
        this.targetReference = monoBehaviour;
        this.targetValue = propertyName;
    }
}
