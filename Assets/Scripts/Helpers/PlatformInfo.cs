using UnityEngine;
/// <summary>
/// the structure to store platforms data
/// </summary>
[CreateAssetMenu(fileName = "NewPlatform", menuName = "BAHMAN/New Platform", order = 2)]
public class PlatformInfo : ScriptableObject
{
    /// <summary>
    /// the name of platform for display purposes
    /// </summary>
    public string PlatformName;

    /// <summary>
    /// the graphical shape of the platform
    /// </summary>
    public Sprite PlatformView;

    /// <summary>
    /// the height of the platform in 3D world
    /// </summary>
    public int PlatformHeight;

    /// <summary>
    /// the type of platform
    /// </summary>
    public PlatformType PlatformType;

    /// <summary>
    /// the offset of each height platform. it multiplies with the height to represent the player position
    /// </summary>
    public float PlayerPositionOffset;

    
}

