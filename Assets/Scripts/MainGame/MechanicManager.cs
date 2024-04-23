using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// perform the mechanics on the list one by one and even loops through the PROC1
/// </summary>
public class MechanicManager : MonoBehaviour
{
    /// <summary>
    /// used for singletone purposes
    /// </summary>
    public static MechanicManager _Instance;

    /// <summary>
    /// the queue to store all mechanics based by MechanicActionAbstract
    /// </summary>
    private Queue<MechanicActionAbstract> _mechanicQueue;

    /// <summary>
    /// two panels on the screen representing the mechanic list
    /// </summary>
    [SerializeField] GameObject _mainPanel, _proc1Panel;

    /// <summary>
    /// the delay between each MechanicActionAbstract
    /// </summary>
    float[] _runSpeeds = { 1f, .1f };

    
    /// <summary>
    /// enum representing the perform speed index
    /// </summary>
    [SerializeField] MechanicPerformSpeed _performSpeed;

    /// <summary>
    /// defines whether the list is running or not. used by GameManager to determin the win or loose condition
    /// </summary>
    bool _isMechanicRunning = false;

    /// <summary>
    /// public handler for _isMechanicRunning
    /// </summary>
    /// <returns></returns>
    public bool _IsListRunning()
    {
        return _isMechanicRunning;
    }


    private void Awake()
    {
        // set the active instance to this code
        if (_Instance == null)
            _Instance = this;
        _mechanicQueue = new Queue<MechanicActionAbstract>();
    }


    /// <summary>
    /// returns the current speed
    /// </summary>
    /// <returns></returns>
    public float _GetWaitTime()
    {
        return _runSpeeds[(int)_performSpeed];
    }


    /// <summary>
    /// adds the mechanics to the queue
    /// </summary>
    /// <param name="iMechanic"></param>
    public void _AddMechanic(MechanicActionAbstract iMechanic)
    {
        _mechanicQueue.Enqueue(iMechanic);
    }

    /// <summary>
    /// reset all the mechanics
    /// </summary>
    public void _RemoveAllMechanic()
    {
        _mechanicQueue.Clear();
    }


    /// <summary>
    /// set the current speed
    /// </summary>
    /// <param name="iNewSpeed">new speed</param>
    public void _SetSpeed(MechanicPerformSpeed iNewSpeed)
    {
        _performSpeed = iNewSpeed;
    }
    
    /// <summary>
    /// loops through all the mechanics based on queue and perform one by one
    /// </summary>
    public async void _RunAllMechanics()
    {
        MechanicActionAbstract MAA;
        _isMechanicRunning = true;
        while (_mechanicQueue.Count > 0)
        {
            MAA = _mechanicQueue.Dequeue();
            await MAA.Perform(LevelLoaderController._Instance._GetPlayerController(), LevelLoaderController._Instance._GetGrid());
            await Task.Delay(A.LevelMap.UnityToCSharpTime(_runSpeeds[(int)_performSpeed]));
            
        }
        _isMechanicRunning = false;
        GameManager._Instance._MechanicListEnded();
    }
    
}



