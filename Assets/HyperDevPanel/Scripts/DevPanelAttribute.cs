using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public class DevPanelAttribute : Attribute
{
    private string FunctionName;

    public DevPanelAttribute(string _functionToCall)
    {
        FunctionName = _functionToCall;
    }
    public string GetMethodName()
    {
        return FunctionName;
    }
}
