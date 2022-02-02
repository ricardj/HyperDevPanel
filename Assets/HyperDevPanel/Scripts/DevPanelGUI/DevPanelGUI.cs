using UnityEngine;

public class DevPanelGUI : MonoBehaviour
{
    public static string Capitalize(string text)
    {
        return char.ToUpper(text[0]) + text.Substring(1);
    }
}
