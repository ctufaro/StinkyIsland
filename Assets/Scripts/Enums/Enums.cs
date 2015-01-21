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
        LostLife,
        GameOver
    }

    public enum Button
    {
        A,
        B,
        C,
        D
    }
}
