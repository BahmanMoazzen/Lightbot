using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    /// <summary>
    /// all the sound FX clips of the game
    /// </summary>
    [SerializeField] AudioClip[] _allFX;
    /// <summary>
    /// for singletone purposes
    /// </summary>
    public static SoundFXManager _Instance;
    /// <summary>
    /// the reference to the AudioSource
    /// </summary>
    AudioSource _playerAudioSource;
    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
            // getting the reference to the AudioSource
            _playerAudioSource = GetComponent<AudioSource>();
        }
    }
    /// <summary>
    /// play a sound FX determinded by iClip. also the playback type via iIsOneShot
    /// </summary>
    /// <param name="iClip">the soundFX clip</param>
    /// <param name="iIsOneShot">define if the playbacktype should be one shot</param>
    public void _PlayFX(SoundFX iClip,bool iIsOneShot)
    {
        if(iIsOneShot)
        {
            _playerAudioSource.PlayOneShot(_allFX[(int)iClip]);
        }
        else
        {
            _playerAudioSource.clip = _allFX[(int)iClip];
            _playerAudioSource.Play();
        }
    }
}

/// <summary>
/// the index definition of the sound FX clips
/// </summary>
public enum SoundFX
{
    Mechanic0, Mechanic1, Mechanic2, Mechanic3, Mechanic4, Mechanic5, Mechanic6, Mechanic7, Mechanic8, Mechanic9, Mechanic10, Mechanic11, Mechanic12
                        , Click, hint, jump, levelIn, levelOut, light, lightFail, lightOn, addMechanicFail, raise, rise, setwin, step, teleport
}
