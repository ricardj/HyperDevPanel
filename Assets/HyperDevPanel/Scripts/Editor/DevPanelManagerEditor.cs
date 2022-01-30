using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DevPanelManager))]
public class DevPanelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DevPanelManager devPanelManager = (DevPanelManager)target;
        if (GUILayout.Button("Generate Dev Panel"))
        {
            devPanelManager.GenerateDevPanelUI();
        }
    }
}
