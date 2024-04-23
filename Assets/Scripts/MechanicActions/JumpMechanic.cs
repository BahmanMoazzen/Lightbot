using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// the jump mechanic implementation
/// </summary>
public class JumpMechanic : MechanicActionAbstract
{
    /// <summary>
    /// the perform method of the mechanic. to run this mechanic it should be called by others.
    /// </summary>
    /// <param name="iPlayer">the player (robot) of the game</param>
    /// <param name="iGrid">the level grid</param>
    /// <returns>cue to determine the accomplishment of the task</returns>
    public override Task Perform(PlayerController iPlayer, List<List<PlatformController>> iGrid)
    {
        // detects the next platform and the position of it
        (PlatformController nextPT, GridPosition newPosition) = A.LevelMap.NextPlatform(iPlayer._GetCurrentPosition(), iPlayer._GetCurrentRotation(), iGrid);
        if (nextPT != null)
        {
            // check if the platform is just one level higher OR the platform is lower
            if (nextPT._GetPlatformHeight() == A.LevelMap.PositionToPlatformController(iPlayer._GetCurrentPosition(), iGrid)._GetPlatformHeight() + 1
                || A.LevelMap.PositionToPlatformController(iPlayer._GetCurrentPosition(), iGrid)._GetPlatformHeight() > nextPT._GetPlatformHeight())
            {
                // jumps to other platform
                nextPT._RelocatePlayer(iPlayer.gameObject);
                iPlayer._SetCurrentPosition(newPosition);

                // ignite sound
                SoundFXManager._Instance._PlayFX(SoundFX.jump, true);
            }

        }
       
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// the aesthetic look of the mechanics
    /// </summary>
    /// <returns>the Sprite to set as the icon face</returns>
    public override Sprite _GetSprite()
    {
        return A.GameSettings.MechanicSprites[(int)MechanicShapes.Jump];
    }
}
