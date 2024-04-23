using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RotateLeftMechanic : MechanicActionAbstract
{
    const int ROTATIONDIRECTION = -1;

    /// <summary>
    /// does the mechanic
    /// </summary>
    /// <param name="iPlayer">the reference to the player</param>
    /// <param name="iGrid">the level grid</param>
    /// <returns>completed task for async purposes</returns>
    public override Task Perform(PlayerController iPlayer, List<List<PlatformController>> iGrid)
    {
        iPlayer._ChangeRotation(A.Player.CalculateRotation(iPlayer._GetCurrentRotation(),ROTATIONDIRECTION));
        // ignite sound
        SoundFXManager._Instance._PlayFX(SoundFX.hint, true);
        return Task.CompletedTask;
    }
    /// <summary>
    /// returns the display of the mechanic
    /// </summary>
    /// <returns></returns>
    public override Sprite _GetSprite()
    {
        return A.GameSettings.MechanicSprites[(int)MechanicShapes.TurnLeft];
    }
}
