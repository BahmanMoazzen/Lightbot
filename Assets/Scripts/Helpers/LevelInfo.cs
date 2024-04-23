using UnityEngine;
/// <summary>
/// the structure to store game level information
/// </summary>
[CreateAssetMenu(fileName = "NewLevel", menuName = "BAHMAN/New Level", order = 1)]
public class LevelInfo : ScriptableObject
{ 
    [Header("Level Config")]
    /// <summary>
    /// the name of level for display purposes
    /// </summary>
    public string _LevelName;

    /// <summary>
    /// the text the level would created upon
    /// </summary>
    public TextAsset _LevelMap;

    /// <summary>
    /// the start position of the robot
    /// </summary>
    public GridPosition _RobotPosition;

    /// <summary>
    /// the starting rotation of the robot
    /// </summary>
    public RobotRotation _StartingRotation;

}

