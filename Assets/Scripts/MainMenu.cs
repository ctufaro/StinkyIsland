using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject playerSelectPanel;
    public GameObject fadePanel;
    
    void Awake()
    {
        GameManager.instance.SetGameState(Enums.GameState.MainMenu);
    }
    
    void Start()
    {
        TogglePanel(false);
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
        //Fade to black than loadlevel
        fadePanel.SetActive(true);
        StartCoroutine(FadeToStart(fadePanel.GetComponent<Image>(), 1f, .3f));
    }

    IEnumerator FadeToStart(Image panel, float aValue, float aTime)
    {
        float alpha, r, g, b = 0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alpha = 0f;
            r = panel.color.r;
            g = panel.color.g;
            b = panel.color.b;
            panel.color = new Color(r, g, b, Mathf.Lerp(alpha, aValue, t));
            yield return null;
        }

        Application.LoadLevel("Level1");
        GameManager.instance.SetGameState(Enums.GameState.GetReady);
    }

    public void TogglePanel(bool active)
    {
        playerSelectPanel.SetActive(active);
    }

    void ContinueGame() { }
    void Settings() { }
    void Credits() { }
}
