using UnityEngine;
using System.Collections;

public class SplashMenu : MonoBehaviour {

    void Awake()
    {
        GameManager.instance.SetGameState(Enums.GameState.Splash);
    }
    
    void OnGUI()
    {
        if (Input.touchCount > 0 || Input.anyKey)
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
