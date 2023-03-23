using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Hyper Scriptables/New Dev Options List")]
public class DevOptionsListSO : ScriptableObject
{
    public List<IDevOptionSO> options;
}
