using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Enums.GameState currentLevel;

    void Awake()
    {
        GameManager.instance.SetGameState(currentLevel);
    }

}
