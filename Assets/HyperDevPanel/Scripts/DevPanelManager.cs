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

        var typesWithMyAttribute =
        from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
        from methods in t.GetMethods()
        let attributes = methods.GetCustomAttributes(typeof(DevPanelAttribute), true)
        where attributes != null && attributes.Length > 0
        select new { Type = t, Attributes = attributes.Cast<DevPanelAttribute>() };

        Debug.Log("So far so good : " + typesWithMyAttribute.Count());
        Debug.Log("Executing ASsembly: " + System.Reflection.Assembly.GetExecutingAssembly());


        foreach (var CurrentType in typesWithMyAttribute)
        {
            Debug.Log("Even further");

            List<MonoBehaviour> monobehaviours = FindObjectsOfType<MonoBehaviour>().Where(m => m.GetType() == CurrentType.Type).ToList();
            for (int i = 0; i < monobehaviours.Count; i++)
            {
                MonoBehaviour currentMonoBehaviour = monobehaviours[i];
                foreach (var customAttribute in CurrentType.Attributes)
                {
                    GenerateNewButton(currentMonoBehaviour, customAttribute.GetMethodName(), customAttribute.GetMethodName());
                }
            }

        }

    }

    public void ClearDevPanel()
    {
        //if (!Application.isPlaying)
        //{
        //    foreach (UnityEngine.Object item in devPanelParent.transform)
        //    {
        //        DestroyImmediate(((Transform)item).gameObject);
        //    }
        //}
        devPanelButtons.Clear();
    }

    public void GenerateNewButton(MonoBehaviour monoBehaviour, string buttonName, string methodName)
    {
        DevPanelButton newDevButton = Instantiate(buttonPrefab, devPanelParent.transform);
        newDevButton.SetButtonText(buttonName);
        newDevButton.SetButtonCallback(monoBehaviour,methodName);
        devPanelButtons.Add(newDevButton);
    }
}
