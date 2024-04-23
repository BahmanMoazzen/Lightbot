using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSetting", menuName = "BAHMAN/New Game Setting", order = 3)]
/// <summary>
/// stores the game seetings
/// </summary>
public class GameSettingInfo : ScriptableObject
{

    
    [Header("Platform Settings")]
    /// <summary>
    /// the empty prefab to show a platform
    /// </summary>
    public GameObject EmptyPlatformPrefab;

    /// <summary>
    /// all platform datas 
    /// </summary>
    public PlatformInfo[] PlatformDatas;


    
    [Header("Robot Settings")]
    /// <summary>
    /// the robot prefab
    /// </summary>
    public GameObject RobotPrefab;

    /// <summary>
    /// the faces of the robot
    /// </summary>
    public Sprite[] RobotSprites;

    /// <summary>
    /// the shapes of mechanics
    /// </summary>
    public Sprite[] MechanicSprites;

    
    [Header("Level Settings")]
    /// <summary>
    /// the position value for generating game scene
    /// </summary>
    public Vector2 MovementFactor = new Vector2(-.46f, .23f);

    /// <summary>
    /// the depth factor for generating game scene
    /// </summary>
    public  Vector2 DepthFactor = new Vector2(-.46f, -.23f);

    /// <summary>
    /// all the levels of the game
    /// </summary>
    public LevelInfo[] AllLevels;

}
