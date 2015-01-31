using UnityEngine;
using System.Collections;

public class Enums : ScriptableObject {
    
    public enum GameState
    {
        Splash,
        MainMenu,
        //Intro,
        //GetReady,
        LevelRunning,
        LevelComplete,
        LostLife,
        GameOver
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
}
