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
    public DevPanelSlider sliderPrefab;
    public DevPanelToogle tooglePrefab;
    public GameObject devPanelParent;

    public List<DevPanelGUI> devPanelGUI;

    public void GenerateDevPanelUI()
    {
        ClearDevPanel();
        GenerateButtonsUsingReflection();
        //GenerateSlidersUsingReflection();
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


        Debug.Log("Method properties: " + typesWithMyAttribute.Count());

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

    public void GenerateSlidersUsingReflection()
    {
        var typesWithMyAttribute =
                from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                    //group t by t.Name
                from properties in t.GetProperties()
                let attributes = properties.GetCustomAttributes(typeof(DevPanelAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<DevPanelAttribute>(), Properties = t.GetProperties() };


        Debug.Log("Property attributes: " + typesWithMyAttribute.Count());

        int propertyindex = 0;
        Type previousType = null;
        foreach (var CurrentType in typesWithMyAttribute)
        {
            List<MonoBehaviour> monobehaviours = FindObjectsOfType<MonoBehaviour>().Where(m => m.GetType() == CurrentType.Type).ToList();
            if (CurrentType.Type != previousType)
            {
                propertyindex = 0;
            }
            for (int i = 0; i < monobehaviours.Count; i++)
            {
                MonoBehaviour currentMonoBehaviour = monobehaviours[i];

                foreach (var customAttribute in CurrentType.Attributes)
                {
                    string propertyName = CurrentType.Properties[propertyindex].Name;
                    Debug.Log("That far");

                    if (CurrentType.GetType() == typeof(float))
                    {
                        Debug.Log("Not that far");
                        GenerateNewSlider(currentMonoBehaviour, propertyName, propertyName);
                    }
                    if (CurrentType.GetType() == typeof(bool))
                    {
                        GenerateNewToggle(currentMonoBehaviour, propertyName, propertyName);
                    }
                    propertyindex++;
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
        devPanelGUI.Clear();
    }

    public void GenerateNewButton(MonoBehaviour monoBehaviour, string buttonName, string methodName)
    {
        DevPanelButton newDevButton = Instantiate(buttonPrefab, devPanelParent.transform);
        newDevButton.SetButtonText(buttonName);
        newDevButton.SetButtonCallback(monoBehaviour, methodName);
        devPanelGUI.Add(newDevButton);
    }

    public void GenerateNewSlider(MonoBehaviour monoBehaviour, string sliderName, string propertyName)
    {
        DevPanelSlider newDevSlider = Instantiate(sliderPrefab, devPanelParent.transform);
        newDevSlider.SetSliderTitle(sliderName);
        newDevSlider.SetReferences(monoBehaviour, propertyName);
        devPanelGUI.Add(newDevSlider);
    }

    public void GenerateNewToggle(MonoBehaviour monoBehaviour, string toggleName, string propertyName)
    {
        DevPanelToogle newDevToogle = Instantiate(tooglePrefab, devPanelParent.transform);
        newDevToogle.SetToogleName(toggleName);
        newDevToogle.SetReferences(monoBehaviour, propertyName);
        devPanelGUI.Add(newDevToogle);
    }
}
