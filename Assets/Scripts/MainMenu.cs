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
        //Rect buttonRect = new Rect((Screen.width / 2) - 70,(2 * Screen.height / 3) - 30,0,0);
        //GUI.Label(buttonRect,(displayLabel) ? "click to start game" : "", style);

        if (Input.touchCount > 0 || Input.anyKey)
        {
            Application.LoadLevel("Level1");
        }
    }

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
