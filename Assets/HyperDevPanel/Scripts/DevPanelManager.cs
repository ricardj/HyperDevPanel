using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DevPanelManager : MonoBehaviour
{
    public Button buttonPrefab;
    public GameObject devPanelParent;

    public void Start()
    {
        
    }
}

[CustomEditor(typeof(DevPanelManager)]
public class DevPanelManagerEditor : Editor
{

}




[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public class DevMethodAttribute : PropertyAttribute
{
    private string ButtonName;
    private string FunctionName;
    private bool Above;
    private Type ClassType;

    public DevMethodAttribute(string _functionToCall)
    {

    }

    public DevMethodAttribute(string _FunctionToCall, System.Type _Type, bool _Above = false)
    {
        ButtonName = "Method: " + _FunctionToCall + "()";
        FunctionName = _FunctionToCall;
        Above = _Above;
        ClassType = _Type;
    }
}


public class RandomManager : MonoBehaviour
{

    [DevMethod("SayHello", "RandomManager")]
    public void SayHello()
    {
        Debug.Log("Hello there");
    }

 
}