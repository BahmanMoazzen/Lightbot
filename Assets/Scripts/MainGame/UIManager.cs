using UnityEngine;

/// <summary>
/// the manager of the UI in the game scene
/// </summary>
public class UIManager : MonoBehaviour
{

    /// <summary>
    /// for singletone purposes
    /// </summary>
    public static UIManager _Instance;


    private void Awake()
    {
        // set the instance reference to this code
        if (_Instance == null)
            _Instance = this;
        // instantiating the current population count of every panels in the game
        _currentCount = new int[_maxCount.Length];

    }

    /// <summary>
    /// loading panel of the game befor game loads is visible
    /// </summary>
    [SerializeField] GameObject _loadingPanel;

    /// <summary>
    /// the win\loose panel
    /// </summary>
    [SerializeField] GameObject _endLevelPanel;

    /// <summary>
    /// end game panel
    /// </summary>
    [SerializeField] GameObject _endGamePanel;


    /// <summary>
    /// the try again buttom
    /// </summary>
    [SerializeField] GameObject _tryAgainButtom;

    /// <summary>
    /// the next level buttom
    /// </summary>
    [SerializeField] GameObject _nextLevelButtom;

    /// <summary>
    /// all the panels of the game including Main and Proc1
    /// </summary>
    [SerializeField] GameObject[] _iconPanels;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] GameObject _iconPrefab;

    /// <summary>
    /// setting the current active panel to Main panel
    /// </summary>
    MainGamePanel _currentActivePanel = MainGamePanel.Main;

    /// <summary>
    /// the ProcManager used to retrieve Mechanics from P1 panel
    /// </summary>
    [SerializeField] ProcManager _P1;

    /// <summary>
    /// max capacity for each of the panels in the game
    /// </summary>
    [SerializeField] int[] _maxCount;

    /// <summary>
    /// the current tenants of the panel
    /// </summary>
    int[] _currentCount;

    /// <summary>
    /// hides the loading screen
    /// </summary>
    public void _HideLoadingScreen()
    {
        _loadingPanel.SetActive(false);
    }
    /// <summary>
    /// shows the end game screen
    /// </summary>
    public void _ShowEndGamePanel()
    {
        _endGamePanel.SetActive(true);
        _HideLoadingScreen();
    }


    /// <summary>
    /// show the end level panel
    /// </summary>
    /// <param name="iWin">the win condition of the game</param>
    public void _ShowEndLevelPanel(bool iWin)
    {
        _endLevelPanel.SetActive(true);
        if (iWin)
        {
            _nextLevelButtom.SetActive(true);
            _tryAgainButtom.SetActive(false);
        }
        else
        {
            _nextLevelButtom.SetActive(false);
            _tryAgainButtom.SetActive(true);
        }
    }


    /// <summary>
    /// set the delay between each Mechanic performed
    /// </summary>
    /// <param name="iIsNormalSpeed"></param>
    public void _SetPerformSpeed(bool iIsNormalSpeed)
    {
        if (iIsNormalSpeed)
        {
            MechanicManager._Instance._SetSpeed(MechanicPerformSpeed.Normal);
        }
        else
        {
            MechanicManager._Instance._SetSpeed(MechanicPerformSpeed.Fast);
        }
    }


    /// <summary>
    /// adding MoveMechanic
    /// </summary>
    public void _AddMoveMechanic()
    {
        _addMechanic(new MoveMechanic());
        
    }
    /// <summary>
    /// adding JumpMechanic
    /// </summary>
    public void _AddJumpMechanic()
    {
        _addMechanic(new JumpMechanic());
        
    }
    /// <summary>
    /// adding RotateLeftMechanic
    /// </summary>
    public void _AddRotateLeftMechanic()
    {
        _addMechanic(new RotateLeftMechanic());
        
    }
    /// <summary>
    /// adding RotateRightMechanic
    /// </summary>
    public void _AddRotateRightMechanic()
    {
        _addMechanic(new RotateRightMechanic());
       

    }
    /// <summary>
    /// adding LightMechanic
    /// </summary>
    public void _AddLightMechanic()
    {
        _addMechanic(new LightMechanic());
        
    }
    /// <summary>
    /// adding P1Mechanic
    /// </summary>
    public void _AddProc1()
    {
        _addMechanic(new P1Mechanic(_P1));
        
    }

    /// <summary>
    /// first checks the capasity of the panel then add to the panel
    /// </summary>
    /// <param name="iMechanic">the mechanic to add</param>
    void _addMechanic(MechanicActionAbstract iMechanic)
    {
        if (_currentCount[(int)_currentActivePanel] >= _maxCount[(int)_currentActivePanel])
        {
            // ignite sound
            SoundFXManager._Instance._PlayFX(SoundFX.addMechanicFail, true);

            return;
        }
        _currentCount[(int)_currentActivePanel]++;

        // ignite sound
        SoundFXManager._Instance._PlayFX((SoundFX)_currentCount[(int)_currentActivePanel], true);

        Instantiate(_iconPrefab, _iconPanels[(int)_currentActivePanel].transform).GetComponent<UIIconController>()._LoadMechanic(iMechanic, _currentActivePanel);
    }
   
    /// <summary>
    /// handles the start mechanic command
    /// </summary>
    public void _StartMechanics()
    {
        MechanicManager._Instance._RemoveAllMechanic();
        foreach(UIIconController icon in _iconPanels[(int)MainGamePanel.Main].GetComponentsInChildren<UIIconController>())
        {
            MechanicManager._Instance._AddMechanic(icon._GetMechanic());
        }
        MechanicManager._Instance._RunAllMechanics();
    }
    
    /// <summary>
    /// sets the active panel
    /// </summary>
    /// <param name="iPanel">zero base index of the panel</param>
    public void _SetActivePanel(int iPanel)
    {
        _currentActivePanel = (MainGamePanel)iPanel;
    }

    /// <summary>
    /// decrease the tenants of the panel
    /// </summary>
    /// <param name="iPanel">panel to decrease from</param>
    public void _DecreaseCount(MainGamePanel iPanel)
    {
        _currentCount[(int)iPanel]--;
    }
    
}
