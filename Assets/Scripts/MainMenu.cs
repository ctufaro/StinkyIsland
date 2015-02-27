using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void Awake()
    {
        GameManager.instance.SetGameState(Enums.GameState.MainMenu);
    }
    
    void Start()
    {

    }

    void OnGUI()
    {
        //if (Input.touchCount > 0 || Input.anyKey)
        //{
        //    StartGame();
        //}
    }

    public void StartGame()
    {
        Application.LoadLevel("Level1");
        GameManager.instance.SetGameState(Enums.GameState.LevelRunning);
    }

    void ContinueGame() { }
    void Settings() { }
    void Credits() { }

}
