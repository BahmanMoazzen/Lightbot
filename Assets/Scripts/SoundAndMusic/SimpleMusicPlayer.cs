using UnityEngine;

/// <summary>
/// simply play a loop music clip
/// </summary>
public class SimpleMusicPlayer : MonoBehaviour
{
    /// <summary>
    /// for singletone purposes
    /// </summary>
    public static SimpleMusicPlayer _Instance;
    /// <summary>
    /// the audio source that plays the music
    /// </summary>
    AudioSource _musicSource;
    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
            // get the reference to the AudioSource
            _musicSource = GetComponent<AudioSource>();
        }
        else
        {
            // destroy the game object to avoid duplication
            Destroy(gameObject);
        }

        // make the game object memory remain
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// starts the playback of music
    /// </summary>
    public void _PlayMusic()
    {
        _musicSource?.Play();
    }
    /// <summary>
    /// stop the playback music
    /// </summary>
    public void StopMusic()
    {
        _musicSource?.Stop();
    }

}
