using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// the abstract class for every mechanic of the game
/// </summary>
public abstract class MechanicActionAbstract 
{
    /// <summary>
    /// returns the display of the mechanic
    /// </summary>
    /// <returns></returns>
    public abstract Sprite _GetSprite();
    
    /// <summary>
    /// does the mechanic
    /// </summary>
    /// <param name="iPlayer">the reference to the player</param>
    /// <param name="iGrid">the level grid</param>
    /// <returns>completed task for async purposes</returns>
    public abstract Task Perform(PlayerController iPlayer,List<List< PlatformController>> iGrid);
}
