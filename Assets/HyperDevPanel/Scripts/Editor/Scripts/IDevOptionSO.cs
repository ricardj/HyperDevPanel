using System.Collections;
using UnityEngine;
namespace HyperDevTool
{
    public abstract class IDevOptionSO : ScriptableObject
    {
        public abstract string DevOptionName { get; }
        public abstract void ExecuteOption();
    }
}