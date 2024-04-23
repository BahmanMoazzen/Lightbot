using UnityEngine;
/// <summary>
/// aesthetic appearance of the platform and position of player (robot)
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class PlatformController : MonoBehaviour
{
    /// <summary>
    /// the information of the platform
    /// </summary>
    PlatformInfo _myPlatformInfo;

    /// <summary>
    /// public handler of the height of platform
    /// </summary>
    /// <returns></returns>
    public int _GetPlatformHeight()
    {
        return _myPlatformInfo.PlatformHeight;
    }
    /// <summary>
    /// public handler of the name of platform
    /// </summary>
    /// <returns></returns>
    public string _GetPlatformName()
    {
        return _myPlatformInfo.PlatformName;
    }
    /// <summary>
    /// public handler of the type of platform
    /// </summary>
    /// <returns></returns>
    public PlatformType _GetPlatformType()
    {
        return _myPlatformInfo.PlatformType;
    }

    /// <summary>
    /// loads graphical display of the player based on iPlatformInfo
    /// </summary>
    /// <param name="iPlatformInfo"></param>
    public void _LoadPlatform(PlatformInfo iPlatformInfo)
    {
        _myPlatformInfo = iPlatformInfo;
        GetComponent<SpriteRenderer>().sprite = _myPlatformInfo.PlatformView;
    }


    /// <summary>
    /// setting the location of the player regarding the platform height
    /// </summary>
    /// <param name="iPlayerGameObject">the player in the scene</param>
    public void _RelocatePlayer(GameObject iPlayerGameObject)
    {
        iPlayerGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (_myPlatformInfo.PlatformHeight * _myPlatformInfo.PlayerPositionOffset), transform.position.z);
    }

}
