using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private GUIStyle style;
    private bool displayLabel;

    void Awake()
    {
        GameManager.instance.SetGameState(Enums.GameState.MainMenu);
    }
    
    void Start()
    {
        SetStyle();
        StartCoroutine("Blink");
    }

    void SetStyle()
    {
        style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 14;
        style.fontStyle = FontStyle.Bold;
    }

    void OnGUI()
    {
        if (Input.touchCount > 0 || Input.anyKey)
        {
            Application.LoadLevel("Level1");
            StartGame();
        }
    }

    void StartGame() 
    {
        GameManager.instance.SetGameState(Enums.GameState.LevelRunning);
    }

    void ContinueGame() { }
    void Settings() { }
    void Credits() { }


    IEnumerator Blink()
    {
        while (true)
        {
            displayLabel = true;
            yield return new WaitForSeconds(.5f);
            displayLabel = false;
            yield return new WaitForSeconds(.5f);
        }
    }
}
