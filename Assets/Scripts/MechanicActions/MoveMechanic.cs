using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveMechanic : MechanicActionAbstract
{
    /// <summary>
    /// does the mechanic
    /// </summary>
    /// <param name="iPlayer">the reference to the player</param>
    /// <param name="iGrid">the level grid</param>
    /// <returns>completed task for async purposes</returns>
    public override Task Perform(PlayerController iPlayer, List<List<PlatformController>> iGrid)
    {
        GridPosition direction = A.Player.MoveForwardDirection(iPlayer._GetCurrentRotation());
        GridPosition nexPosition = iPlayer._GetCurrentPosition() + direction;

        // the position format is like Grid[y][X]

        try
        {
            PlatformController nextPT = iGrid[nexPosition.y][nexPosition.x];
            if (nextPT != null) 
            {
                if (iGrid[nexPosition.y][nexPosition.x]._GetPlatformHeight() == iGrid[iPlayer._GetCurrentPosition().y][iPlayer._GetCurrentPosition().x]._GetPlatformHeight())
                {
                    nextPT._RelocatePlayer(iPlayer.gameObject);
                    iPlayer._SetCurrentPosition(nexPosition);

                    // ignite sound
                    SoundFXManager._Instance._PlayFX(SoundFX.step, true);
                }

            }
            else
            {
                // ignite sound
                SoundFXManager._Instance._PlayFX(SoundFX.rise, true);
                Debug.Log("Not the same height");
            }

        }
        catch
        {
            // ignite sound
            SoundFXManager._Instance._PlayFX(SoundFX.rise, true);
            Debug.Log("OutOfBound");
        }
        return Task.CompletedTask;

    }
    /// <summary>
    /// returns the display of the mechanic
    /// </summary>
    /// <returns></returns>
    public override Sprite _GetSprite()
    {
        return A.GameSettings.MechanicSprites[(int)MechanicShapes.Move];
    }
}
