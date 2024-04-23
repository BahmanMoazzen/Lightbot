using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightMechanic : MechanicActionAbstract
{
    
    /// <summary>
    /// the constant value for increase
    /// </summary>
    const int COUNTINCREASE = 1;
    /// <summary>
    /// the constant value for decrease
    /// </summary>
    const int COUNTDECREASE = -1;
    /// <summary>
    /// the perform method of the mechanic. to run this mechanic it should be called by others.
    /// </summary>
    /// <param name="iPlayer">the player (robot) of the game</param>
    /// <param name="iGrid">the level grid</param>
    /// <returns>cue to determine the accomplishment of the task</returns>
    public override Task Perform(PlayerController iPlayer, List<List<PlatformController>> iGrid)
    {
        if (A.Platforms.IsPlatformType(iPlayer._GetCurrentPosition(),iGrid,PlatformType.Light))
        {
            PlatformController currentPlatform = A.LevelMap.PositionToPlatformController(iPlayer._GetCurrentPosition(), iGrid);
            currentPlatform._LoadPlatform(A.Platforms.GetLightedPlatform(currentPlatform._GetPlatformName()));
            GameManager._Instance._CountLighted(COUNTINCREASE);

            // ignite sound
            SoundFXManager._Instance._PlayFX(SoundFX.lightOn, true);
        }
        else if(A.Platforms.IsPlatformType(iPlayer._GetCurrentPosition(), iGrid, PlatformType.Lighted))
        {
            PlatformController currentPlatform = A.LevelMap.PositionToPlatformController(iPlayer._GetCurrentPosition(), iGrid);
            currentPlatform._LoadPlatform(A.Platforms.GetLightPlatform(currentPlatform._GetPlatformName()));
            GameManager._Instance._CountLighted(COUNTDECREASE);

            // ignite sound
            SoundFXManager._Instance._PlayFX(SoundFX.light, true);
        }
        else
        {
            // ignite sound
            SoundFXManager._Instance._PlayFX(SoundFX.lightFail, true);
        }
        return Task.CompletedTask;
    }
    /// <summary>
    /// the aesthetic look of the mechanics
    /// </summary>
    /// <returns>the Sprite to set as the icon face</returns>
    public override Sprite _GetSprite()
    {
        return A.GameSettings.MechanicSprites[(int)MechanicShapes.Light];
    }
}
