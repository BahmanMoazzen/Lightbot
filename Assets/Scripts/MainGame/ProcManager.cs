using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages the Proc1 panel
/// </summary>
public class ProcManager : MonoBehaviour
{
    /// <summary>
    /// the reference to proc1 GameObject
    /// </summary>
    [SerializeField] GameObject _ProcPanel;

    /// <summary>
    /// public handler for mechanics on the Proc1 list
    /// </summary>
    /// <returns></returns>
    public MechanicActionAbstract[] _GetProcMechanics()
    {
        List<MechanicActionAbstract> allMechanics = new List<MechanicActionAbstract>();

        // looping through each icon and extract the mechanic from it then return the list
        foreach (UIIconController icon in _ProcPanel.GetComponentsInChildren<UIIconController>())
        {
            allMechanics.Add(icon._GetMechanic());
        }
        return allMechanics.ToArray();
    }
}
