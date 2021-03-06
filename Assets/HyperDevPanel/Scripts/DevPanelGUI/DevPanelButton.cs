using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevPanelButton : DevPanelGUI
{
    public Button button;
    public TextMeshProUGUI buttonText;

    public string methodName;
    public MonoBehaviour targetReference;
    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            targetReference.Invoke(methodName, 0);
        });
    }

    public void SetButtonText(string text)
    {
        buttonText.text = Capitalize(text);
    }

    public void SetButtonCallback(MonoBehaviour reference, string methodName)
    {
        this.methodName = methodName;
        targetReference = reference;

    }
}
