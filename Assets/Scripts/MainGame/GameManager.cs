using System.Collections;
using UnityEngine;

/// <summary>
/// controlls the follow of game. win lose condition implemented
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// for singletone purposes
    /// </summary>
    public static GameManager _Instance;

    /// <summary>
    /// total number of lamps on game
    /// </summary>
    int _lampCount;

    /// <summary>
    /// total number of lamps that lighted
    /// </summary>
    int _lampLightedCount;

    /// <summary>
    /// if the game won, this would became true
    /// </summary>
    bool _isLevelWon = false;


    private void Start()
    {
        // passing the LevelInfo to load to the LevelLoaderController
        if (A.LeveltoLoad < A.GameSettings.AllLevels.Length)
            LevelLoaderController._Instance._LoadLevel(A.GameSettings.AllLevels[A.LeveltoLoad]);
        else
            UIManager._Instance._ShowEndGamePanel();
    }
    private void Awake()
    {
        // assigning this script as GameManager active script
        if (_Instance == null)
            _Instance = this;
    }
    /// <summary>
    /// increases the number of lamps on the level
    /// </summary>
    public void _CountLamp()
    {
        _lampCount++;
    }
    /// <summary>
    /// counts and uncounts the lighted platforms
    /// </summary>
    /// <param name="iDirection">the definition to count or uncount 1=count -1 = uncount</param>
    public void _CountLighted(int iDirection)
    {
        _lampLightedCount += iDirection;
        if (_lampCount == _lampLightedCount)
        {
            StartCoroutine(_gameWonRoutine());
        }
    }

    

    /// <summary>
    /// fires when game won, need to be async to wait for the end of list MechanicManager cue.
    /// </summary>
    /// <returns></returns>
    IEnumerator _gameWonRoutine()
    {
        _isLevelWon = true;

        // waits for the end of list cue
        yield return new WaitWhile(() => MechanicManager._Instance._IsListRunning());

        // ignite sound
        SoundFXManager._Instance._PlayFX(SoundFX.setwin, true);

        // showing end game panel
        UIManager._Instance._ShowEndLevelPanel(true);
        yield return null;

    }

    /// <summary>
    /// fires after LoadLevelController loads all the level platforms and places the player
    /// </summary>
    /// <param name="iTotalLamps">total number of lamps on the level</param>
    public void _SceneLoaded(int iTotalLamps)
    {
        // setting the total lamps spontanously
        _lampCount = iTotalLamps;
        UIManager._Instance._HideLoadingScreen();


    }
    /// <summary>
    /// fires when all the mechanics list is performed by MechanicManager
    /// </summary>
    public void _MechanicListEnded()
    {
        
        if (!_isLevelWon)
        {
            UIManager._Instance._ShowEndLevelPanel(false);
            Debug.Log("LevelLoos!");

            // ignite sound
            SoundFXManager._Instance._PlayFX(SoundFX.raise, true);
        }
    }

    /// <summary>
    /// increases the currentlevel index and load the new index by simply refresh the scene
    /// </summary>
    public void _LoadNextLevel()
    {
        // ignite sound
        SoundFXManager._Instance._PlayFX(SoundFX.levelOut, true);

        A.LeveltoLoad++;


        A.Scene.LoadScene(ScenesOrder.GameLevel);


    }
    /// <summary>
    /// simply refreshes the scene to reload the level
    /// </summary>
    public void _ReloadLevel()
    {

        A.Scene.LoadScene(ScenesOrder.GameLevel);
    }
    /// <summary>
    /// load the TitleScreen scene
    /// </summary>
    public void _BackToTitle()
    {
        A.Scene.LoadScene(ScenesOrder.TitleScreen);
    }
}
