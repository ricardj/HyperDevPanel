using UnityEngine;

public class SayHelloThereDevTool : MonoBehaviour, IDevTool
{

    [SerializeField] string _toolFunctionality = "Say Hello";
    public void DrawOptions()
    {
        if (GUILayout.Button(_toolFunctionality))
        {
            Debug.Log(_toolFunctionality);
        }
    }

    public string GetToolName()
    {
        return _toolFunctionality;
    }
}