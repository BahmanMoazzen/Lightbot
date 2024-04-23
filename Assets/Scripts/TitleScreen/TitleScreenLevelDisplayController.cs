using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this class used to populate the icon of the levels
/// </summary>
public class TitleScreenLevelDisplayController : MonoBehaviour
{
    /// <summary>
    /// the Text compounent to show the titel of the level
    /// </summary>
    [SerializeField] Text _titleText;

    /// <summary>
    /// the Text compounent to show the number of the level
    /// </summary>
    [SerializeField] Text _levelNumberText;

    /// <summary>
    /// the level order in all level array to show which one should be loaded if clicked
    /// </summary>
    int _myLevelOrder;

    /// <summary>
    /// loading the level information into view of prefab
    /// </summary>
    /// <param name="iLevelCount">the level index to load</param>
    public void _LoadLevelInfo(int iLevelCount)
    {
        // stores the level index in the level array 
        _myLevelOrder = iLevelCount;

        // set the texes accordingly
        _titleText.text = A.GameSettings.AllLevels[iLevelCount]._LevelName;
        _levelNumberText.text = (iLevelCount+1).ToString();
    }
    /// <summary>
    /// by clicking the icon it loads the level
    /// </summary>
    public void _loadLevel()
    {
        // setting the level to load of the logic layer of the game
        A.LeveltoLoad = _myLevelOrder;

        // ignite sound
        SoundFXManager._Instance._PlayFX(SoundFX.levelOut, true);

        // changing the scene
        A.Scene.LoadScene(ScenesOrder.GameLevel);
    }
}
