using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// to store the direction which player is facing
    /// </summary>
    RobotRotation _myCurrentRotation;

    /// <summary>
    /// the reference to SpriteRenderer to change the face of the player
    /// </summary>
    SpriteRenderer _myImage;

    /// <summary>
    /// the position of the player of map grid
    /// </summary>
    GridPosition _currentPosition;

    /// <summary>
    /// the map grid 
    /// </summary>
    List<List<PlatformController>> _grid;

    /// <summary>
    /// position read only
    /// </summary>
    /// <returns></returns>
    public GridPosition _GetCurrentPosition()
    {
        return _currentPosition;
    }

    /// <summary>
    /// rotation read only
    /// </summary>
    /// <returns></returns>
    public RobotRotation _GetCurrentRotation()
    {
        return _myCurrentRotation;
    }

    void Awake()
    {
        // refere to the SpriteRenderer of the GameObject
        _myImage = GetComponent<SpriteRenderer>();

    }

    /// <summary>
    /// setting the new rotation
    /// </summary>
    /// <param name="iRotation">new rotation based on RobotRotation</param>
    public void _ChangeRotation(RobotRotation iRotation)
    {
        _myCurrentRotation = iRotation;
        _myImage.sprite = A.GameSettings.RobotSprites[(int)_myCurrentRotation];

    }

    

    /// <summary>
    /// store  the position of robot and the map grid in proper variables
    /// </summary>
    /// <param name="iGrid">the grid of the level</param>
    /// <param name="iPosition">the position to reside on</param>
    public void _SetCurrentPosition(List<List<PlatformController>> iGrid, GridPosition iPosition)
    {
        _currentPosition = iPosition;
        _grid = iGrid;
    }


    /// <summary>
    /// the other morph of the _SetCurrentPosition
    /// </summary>
    /// <param name="iPosition">the position to reside on</param>
    public void _SetCurrentPosition(GridPosition iPosition)
    {
        _currentPosition = iPosition;

    }

}
