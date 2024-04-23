using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Logic layer of the game
/// </summary>
public static class A
{
    /// <summary>
    /// used by game scene to load the proper level. set by title screen manager level icons
    /// </summary>
    public static int LeveltoLoad = 0;
    /// <summary>
    /// local variable to store loaded game setting informations
    /// </summary>
    private static GameSettingInfo gameSettings;
    /// <summary>
    /// the game setting and materials for the game reference
    /// </summary>
    public static GameSettingInfo GameSettings
    {
        get
        {
            if (gameSettings == null)
            {
                gameSettings = Resources.Load<GameSettingInfo>("GameSettings");
            }

            return gameSettings;

        }
    }
    /// <summary>
    /// the scene management logic
    /// </summary>
    public static class Scene
    {
        /// <summary>
        /// loading proper scene
        /// </summary>
        /// <param name="iScene">the scene to load</param>
        public static void LoadScene(ScenesOrder iScene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)iScene);
        }
    }
    /// <summary>
    /// the logic needed to handle player
    /// </summary>
    public static class Player
    {
        /// <summary>
        /// the direction in which the player must move forward
        /// </summary>
        /// <param name="iCurrentRotation">the current rotation of the player</param>
        /// <returns></returns>
        public static GridPosition MoveForwardDirection(RobotRotation iCurrentRotation)
        {

            switch (iCurrentRotation)
            {
                case RobotRotation.DownLeft:
                    return new GridPosition(0, 1);

                case RobotRotation.UpLeft:
                    return new GridPosition(-1, 0);

                case RobotRotation.UpRight:

                    return new GridPosition(0, -1);
                case RobotRotation.DownRight:

                    return new GridPosition(1, 0);

                default: return new GridPosition(0, 0);

            }
        }
        /// <summary>
        /// gets the current rotation of the robot and calculates the next rotation based on turn direction
        /// </summary>
        /// <param name="iCurrentRotation">the current rotation of the player</param>
        /// <param name="iTurnDirection">the rotation direction 1 = turn right -1 = turn left</param>
        /// <returns></returns>
        public static RobotRotation CalculateRotation(RobotRotation iCurrentRotation, int iTurnDirection)
        {
            int newRotation = (int)iCurrentRotation;
            newRotation += iTurnDirection;
            if (newRotation > (int)RobotRotation.DownRight)
            {
                newRotation = 0;

            }
            else if (newRotation < 0)
            {
                newRotation = (int)RobotRotation.DownRight;
            }

            return (RobotRotation)newRotation;
        }
        
    }
    /// <summary>
    /// Stores the logic needed to handle platforms
    /// </summary>
    public static class Platforms
    {
        public static int TotalNumberOfPlatforms = 5;
        /// <summary>
        /// calculates the lighted platform index in the platformDatas
        /// </summary>
        /// <param name="iPlatformName">the platform name player currently stands on</param>
        /// <returns></returns>
        public static int GetLightedPlatformIndex(string iPlatformName)
        {
            int c = 0;
            foreach (PlatformInfo pi in GameSettings.PlatformDatas)
            {
                if (pi.PlatformName == iPlatformName)
                {
                    break;
                }
                c++;
            }
            return c + TotalNumberOfPlatforms;
        }
        /// <summary>
        /// calculates the light platform index in the platformDatas
        /// </summary>
        /// <param name="iPlatformName">the platform name player currently stands on</param>
        /// <returns></returns>
        public static int GetLightPlatformIndex(string iPlatformName)
        {
            int c = 0;
            foreach (PlatformInfo pi in GameSettings.PlatformDatas)
            {
                if (pi.PlatformName == iPlatformName)
                {
                    break;
                }
                c++;
            }
            return c - TotalNumberOfPlatforms;
        }
        /// <summary>
        /// returns the equivalent platform for lightplatform to turn on
        /// </summary>
        /// <param name="iPlatformName">the name of the platform that player stands on</param>
        /// <returns></returns>
        public static PlatformInfo GetLightedPlatform(string iPlatformName)
        {
            return gameSettings.PlatformDatas[GetLightedPlatformIndex(iPlatformName)];
        }
        /// <summary>
        /// returns the equivalent platform for lightedplatform to turn off
        /// </summary>
        /// <param name="iPlatformName">the name of the platform that player stands on</param>
        /// <returns></returns>
        public static PlatformInfo GetLightPlatform(string iPlatformName)
        {
            return gameSettings.PlatformDatas[GetLightPlatformIndex(iPlatformName)];
        }
        /// <summary>
        /// the logic to determine if the player can jump from any platform to any other
        /// </summary>
        /// <param name="iCurrentPlatformHeight">the platformcontrollerheight on which player stands</param>
        /// <param name="iNextPlatformHeight">the next platform intended to jump onto</param>
        /// <returns></returns>
        public static bool CanJump(int iCurrentPlatformHeight, int iNextPlatformHeight)
        {
            if (iCurrentPlatformHeight > iNextPlatformHeight) return true;
            else if (iNextPlatformHeight - iCurrentPlatformHeight == 1) return true;
            else return false;
        }
        /// <summary>
        /// checks the platform type and return the proper bool value
        /// </summary>
        /// <param name="iPosition">this position of the platform on grid</param>
        /// <param name="iGrid">the grid of platforms</param>
        /// <param name="iTypeToCheck">the desire platform type to test upon</param>
        /// <returns></returns>
        public static bool IsPlatformType(GridPosition iPosition,List<List<PlatformController>> iGrid,PlatformType iTypeToCheck)
        {
            PlatformController pc = A.LevelMap.PositionToPlatformController(iPosition, iGrid);
            return pc._GetPlatformType() == iTypeToCheck;
        }
    }
    /// <summary>
    /// the logic needed to handle the map of the game
    /// </summary>
    public static class LevelMap
    {
        /// <summary>
        /// change Unity time to CSharp milisecond
        /// </summary>
        /// <param name="iTime">the time amount to convert</param>
        /// <returns></returns>
        public static int UnityToCSharpTime(float iTime)
        {
            return (int)iTime * 1000;
        }
        
        /// <summary>
        /// calculates the next position on the grid based on current position and rotation of the robot. returns the new PlatformController and position
        /// </summary>
        /// <param name="iCurrentLocation">the current position of the robot</param>
        /// <param name="iCurrentRotation">the current rotation of the robot</param>
        /// <param name="iGrid">the grid of PlatformControllers</param>
        /// <returns></returns>
        public static (PlatformController, GridPosition) NextPlatform(GridPosition iCurrentLocation, RobotRotation iCurrentRotation, List<List<PlatformController>> iGrid)
        {
            GridPosition direction = A.Player.MoveForwardDirection(iCurrentRotation);
            GridPosition nexPosition = iCurrentLocation + direction;

            // the position format is like Grid[y][X]
            try
            {
                PlatformController nextPT = iGrid[nexPosition.y][nexPosition.x];

                return (nextPT, nexPosition);
            }
            catch
            {
                return (null, new GridPosition(0, 0));
            }


        }

        /// <summary>
        /// text file character separator for rows
        /// </summary>
        private static readonly char FILEROWSEPARATOR = ';';
        /// <summary>
        /// text file character separator for columns
        /// </summary>
        private static readonly char FILECOLUMNSEPARATOR = ',';
        /// <summary>
        /// converts the position on the grid into a PlatformController
        /// </summary>
        /// <param name="iPosition">the position on the grid</param>
        /// <param name="iGrid">the grid to return the PlatformController</param>
        /// <returns></returns>
        public static PlatformController PositionToPlatformController(GridPosition iPosition,List<List<PlatformController>> iGrid)
        {
            return iGrid[iPosition.y][iPosition.x];
        }
        /// <summary>
        /// converts a text file into a two dimension list of PlatformInfo
        /// </summary>
        /// <param name="iLevelTextFile">the text file to read from</param>
        /// <returns></returns>
        public static List<List<PlatformInfo>> TextToGrid(TextAsset iLevelTextFile)
        {
            List<List<PlatformInfo>> levelArray = new List<List<PlatformInfo>>();
            // reading all rows separated by FILEROWSEPARATOR
            string[] rows = iLevelTextFile.text.Trim().Split(FILEROWSEPARATOR);
            int totalRow = 0, totalColumn = 0;
            foreach (string row in rows)
            {
                if (row.Length > 0)
                {
                    totalColumn = 0;
                    levelArray.Add(new List<PlatformInfo>());
                    // reading all rows separated by FILECOLUMNSEPARATOR
                    string[] colums = row.Trim().Split(FILECOLUMNSEPARATOR);
                    foreach (string column in colums)
                    {
                        if (column.Length > 0)
                        {
                            int theNumber = int.Parse(column.Trim());
                            if (theNumber >= 0)
                                // indicates a platform
                                levelArray[totalRow].Add(GameSettings.PlatformDatas[theNumber]);
                            else
                                // indicates an empty space
                                levelArray[totalRow].Add(null);
                            // next column
                            totalColumn++;
                        }
                    }
                    // next row
                    totalRow++;
                }

            }

            return levelArray;
        }
    }
}
/// <summary>
/// the structure to store the position of anything on a two dimension grid
/// + - * operators also implemented.
/// </summary>
[Serializable]
public struct GridPosition
{
    public int x;
    public int y;
    public GridPosition(int iX, int iY)
    {
        x = iX; y = iY;
    }
    public static GridPosition operator +(GridPosition a) => a;
    public static GridPosition operator -(GridPosition a) => new GridPosition(-a.x, -a.y);
    public static GridPosition operator +(GridPosition a, GridPosition b) => new GridPosition(a.x + b.x, a.y + b.y);
    public static GridPosition operator -(GridPosition a, GridPosition b) => new GridPosition(a.x - b.x, a.y - b.y);
    public static GridPosition operator *(GridPosition a, int b) => new GridPosition(a.x * b, a.y * b);
    
}

/// <summary>
/// all the scenes in the game in order of appearance in build list.
/// </summary>
public enum ScenesOrder { TitleScreen, GameLevel }

/// <summary>
/// all the available rotations of the robot
/// </summary>
public enum RobotRotation { DownLeft, UpLeft, UpRight, DownRight }

/// <summary>
///  all the platform types
/// </summary>
public enum PlatformType { Simple, Light, Lighted }

/// <summary>
/// the definition for delay between commands
/// </summary>
public enum MechanicPerformSpeed { Normal,Fast}

/// <summary>
/// the index describer for Mechanics Sprits
/// </summary>
public enum MechanicShapes { Move,TurnLeft,TurnRight,Jump,Light,P1}

/// <summary>
/// index describer of main game panel
/// </summary>
[Serializable]
public enum MainGamePanel { Main,P1}