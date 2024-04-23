using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderController : MonoBehaviour
{
    /// <summary>
    /// the instance of the class used for singletone purposes
    /// </summary>
    public static LevelLoaderController _Instance;


    /// <summary>
    /// the LevelInfo of the current level stored for further use
    /// </summary>
    [SerializeField] LevelInfo _lvlToLoad;

    /// <summary>
    /// the grid of the level stored for the further referencing
    /// </summary>
    List<List<PlatformController>> _levelPltformControllers;


    /// <summary>
    /// the current Player loaded into scene
    /// </summary>
    GameObject _playerGameObject;

    /// <summary>
    /// the access ahndler for Grid loaded into scene
    /// </summary>
    /// <returns></returns>
    public List<List<PlatformController>> _GetGrid()
    {
        return _levelPltformControllers;
    }
    /// <summary>
    /// the handle to access the PlayerController scripts on the level
    /// </summary>
    /// <returns></returns>
    public PlayerController _GetPlayerController()
    {
        return _playerGameObject.GetComponent<PlayerController>();
    }

    /// <summary>
    /// setting the singletone _Instance
    /// </summary>
    private void Awake()
    {
        if (_Instance == null) _Instance = this;
    }

    /// <summary>
    /// this method is calling from other scripts
    /// </summary>
    /// <param name="iLevel"></param>
    public void _LoadLevel(LevelInfo iLevel)
    {
        StartCoroutine(_LoadScene(iLevel));
    }
    /// <summary>
    /// the coroutine to load the text level from file async
    /// </summary>
    /// <param name="iLevel">the level info to load from</param>
    /// <returns>Nothing</returns>
    IEnumerator _LoadScene(LevelInfo iLevel)
    {
        yield return 0;
        _lvlToLoad = iLevel;

        // converting text file into a two dimension List of PlatformInfos
        List<List<PlatformInfo>> levelArray = A.LevelMap.TextToGrid(_lvlToLoad._LevelMap);

        //populating the two dimension List of PlatformControllers
        _levelPltformControllers = new List<List<PlatformController>>();

        // setting the initial values of the grid
        int depthCountX = 0, depthCountY = 0;
        float horPos = 0f, verPos = 0f;
        int zCount = 0;
        int lampsOnLevel = 0;

        for (int i = 0; i < levelArray.Count; i++)
        {
            // adding new row to the grid (Y dimension)
            _levelPltformControllers.Add(new List<PlatformController>());
            for (int j = 0; j < levelArray[i].Count; j++)
            {

                if (levelArray[i][j] != null)
                {
                    if (levelArray[i][j].PlatformType == PlatformType.Light)
                    {
                        lampsOnLevel++;
                    }
                    // calculating location on the scene
                    Vector3 instantiatePosition = new Vector3(horPos + (depthCountX * A.GameSettings.DepthFactor.x), verPos + (depthCountY * A.GameSettings.DepthFactor.y), zCount);
                    // instantiating the platform skeleton
                    GameObject go = Instantiate(A.GameSettings.EmptyPlatformPrefab, instantiatePosition, Quaternion.identity);
                    // setting up the platform via PlatformInfo data
                    go.GetComponent<PlatformController>()._LoadPlatform(levelArray[i][j]);
                    // adding platform to the grid (X dimension)
                    _levelPltformControllers[i].Add(go.GetComponent<PlatformController>());

                }
                else
                {
                    // adding null platform for nonvisible ones to maintain the square dimension
                    _levelPltformControllers[i].Add(null);
                }
                depthCountY++;
                zCount--;
                horPos += A.GameSettings.MovementFactor.x;
            }
            verPos += A.GameSettings.MovementFactor.y;
            horPos = 0;
            depthCountY = 0;
            depthCountX++;
        }

        // instantiating the player (robot head)
        _playerGameObject = Instantiate(A.GameSettings.RobotPrefab, Vector3.zero, Quaternion.identity);
        _playerGameObject.GetComponent<PlayerController>()._ChangeRotation(_lvlToLoad._StartingRotation);
        _playerGameObject.GetComponent<PlayerController>()._SetCurrentPosition(_levelPltformControllers, _lvlToLoad._RobotPosition);

        // setting player to default location
        A.LevelMap.PositionToPlatformController(_lvlToLoad._RobotPosition, _levelPltformControllers)._RelocatePlayer(_playerGameObject);

        // set the maximum lights on scene
        GameManager._Instance._SceneLoaded(lampsOnLevel);

        // ignite sound
        SoundFXManager._Instance._PlayFX(SoundFX.teleport, true);


    }
}

