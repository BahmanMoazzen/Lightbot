using System.Collections;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    /// <summary>
    /// the parent UI GameObject to instantiate level menu icon under it
    /// </summary>
    [SerializeField] GameObject _levelDisplayParentGameObject;

    /// <summary>
    /// the level icon prefab to instantiate from
    /// </summary>
    [SerializeField] GameObject _levelDisplayPrefab;

    /// <summary>
    /// make start callback IEnumerator to avoid freezing in the first screen of the game
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        // looping through all level of the game and creating menu icon based on _levelDisplayPrefab and parent it under _levelDisplayParentGameObject
        for (int i = 0; i < A.GameSettings.AllLevels.Length; i++)
        {
            Instantiate(_levelDisplayPrefab, _levelDisplayParentGameObject.transform).GetComponent<TitleScreenLevelDisplayController>()._LoadLevelInfo(i);
            yield return null;
        }

        // play loaded sound
        SoundFXManager._Instance._PlayFX(SoundFX.teleport,true);
        
    }

    
}
