using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// controlls every icon on the Main and Proc1 list
/// </summary>
public class UIIconController : MonoBehaviour
{
    /// <summary>
    /// mechanic inside the icon
    /// </summary>
    MechanicActionAbstract _mechanic;

    /// <summary>
    /// the image reference of the icon
    /// </summary>
    [SerializeField] Image _icon;

    /// <summary>
    /// the panel of icon residence
    /// </summary>
    MainGamePanel _mainGamePanel;

    /// <summary>
    /// the public handler of the mechanic
    /// </summary>
    /// <returns></returns>
    public MechanicActionAbstract _GetMechanic()
    {
        return _mechanic;
    }

    /// <summary>
    /// loads the image and the mechanic into icon
    /// </summary>
    /// <param name="iMechanic">the mechanic of the icon</param>
    /// <param name="iPanel">the panel in which icon resides</param>
    public void _LoadMechanic(MechanicActionAbstract iMechanic,MainGamePanel iPanel)
    {
        _mechanic = iMechanic;
        _icon.sprite = _mechanic._GetSprite();
        _mainGamePanel = iPanel;
    }

    /// <summary>
    /// removes the icon from list
    /// </summary>
    public void _RemoveFromList()
    {
        UIManager._Instance._DecreaseCount(_mainGamePanel);
        Destroy(gameObject);
    }
}
