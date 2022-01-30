using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class DevPanelManager : MonoBehaviour
{
    public DevPanelButton buttonPrefab;
    public GameObject devPanelParent;

    public List<DevPanelButton> devPanelButtons;

    public void GenerateDevPanelUI()
    {
        ClearDevPanel();
        GenerateButtonsUsingReflection();
    }

    private void GenerateButtonsUsingReflection()
    {
        var typesWithMyAttribute =
                from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                    //group t by t.Name
                from methods in t.GetMethods()
                let attributes = methods.GetCustomAttributes(typeof(DevPanelAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<DevPanelAttribute>(), Methods = t.GetMethods() };


        Debug.Log("So far so good : " + typesWithMyAttribute.Count());
        Debug.Log("Executing ASsembly: " + System.Reflection.Assembly.GetExecutingAssembly());

        int methodIndex = 0;
        Type previousType = null;
        foreach (var CurrentType in typesWithMyAttribute)
        {
            List<MonoBehaviour> monobehaviours = FindObjectsOfType<MonoBehaviour>().Where(m => m.GetType() == CurrentType.Type).ToList();
            if (CurrentType.Type != previousType)
            {
                methodIndex = 0;
            }
            for (int i = 0; i < monobehaviours.Count; i++)
            {
                MonoBehaviour currentMonoBehaviour = monobehaviours[i];

                foreach (var customAttribute in CurrentType.Attributes)
                {
                    string methodName = CurrentType.Methods[methodIndex].Name;
                    GenerateNewButton(currentMonoBehaviour, methodName, methodName);
                    methodIndex++;
                }
            }
            previousType = CurrentType.Type;
        }
    }

    public void ClearDevPanel()
    {
        if (!Application.isPlaying)
        {

            for (int i = this.devPanelParent.transform.childCount; i > 0; --i)
                DestroyImmediate(this.devPanelParent.transform.GetChild(0).gameObject);
        }
        devPanelButtons.Clear();
    }

    public void GenerateNewButton(MonoBehaviour monoBehaviour, string buttonName, string methodName)
    {
        DevPanelButton newDevButton = Instantiate(buttonPrefab, devPanelParent.transform);
        newDevButton.SetButtonText(buttonName);
        newDevButton.SetButtonCallback(monoBehaviour, methodName);
        devPanelButtons.Add(newDevButton);
    }
}
