using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class P1Mechanic : MechanicActionAbstract
{
    ProcManager _procManager;

    /// <summary>
    /// constructor of the P1Mechanic
    /// </summary>
    /// <param name="iProcManager">the reference to the Proc1 panel</param>
    public P1Mechanic(ProcManager iProcManager)
    {
        _procManager = iProcManager;
    }
    /// <summary>
    /// does the mechanic
    /// </summary>
    /// <param name="iPlayer">the reference to the player</param>
    /// <param name="iGrid">the level grid</param>
    /// <returns>completed task for async purposes</returns>
    public override async Task Perform(PlayerController iPlayer, List<List<PlatformController>> iGrid)
    {
        // reading all mechanics of Proc1 Panel by ProcManager reference
        MechanicActionAbstract[] allMechanics = _procManager._GetProcMechanics();
        // looping through the Proc1 mechanics and do them one by one 
        for (int i = 0; i < allMechanics.Length; i++)
        {
            // performing the mechanic
            await allMechanics[i].Perform(iPlayer, iGrid);
            // wait for proper amount of time if it is not the last one
            if (i < allMechanics.Length - 1)
                await Task.Delay(A.LevelMap.UnityToCSharpTime(MechanicManager._Instance._GetWaitTime()));
        }


    }
    /// <summary>
    /// returns the display of the mechanic
    /// </summary>
    /// <returns></returns>
    public override Sprite _GetSprite()
    {
        return A.GameSettings.MechanicSprites[(int)MechanicShapes.P1];
    }
}
