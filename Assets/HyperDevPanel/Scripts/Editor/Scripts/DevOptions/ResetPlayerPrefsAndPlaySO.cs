using UnityEditor;
using UnityEngine;
namespace HyperDevTool
{

    [CreateAssetMenu(menuName = "Hyper Scriptables/New Reset Player Prefs and play")]
    public class ResetPlayerPrefsAndPlaySO : IDevOptionSO
    {
        public override string DevOptionName => "Reset Player Prefs and Play";

        public override void ExecuteOption()
        {
            PlayerPrefs.DeleteAll();
            EditorApplication.EnterPlaymode();
        }
    }

}