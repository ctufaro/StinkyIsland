using UnityEngine;
using System.Collections;

public class Enums : ScriptableObject {
    
    public enum GameState
    {
        Splash,
        MainMenu,
        Intro,
        GetReady,
        LevelRunning,
        LevelComplete,
        LostLife,
        GameOver,
        LevelReset
    }

    public enum MonsterType
    {
        Stenchie,
        Trollie
    }


    public enum Button
    {
        A,
        B,
        C,
        D
    }

    public enum Direction
    {
        Up = 1,
        Left = 2,
        Down = 3,
        Right = 4
    }
}
