using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Singleton Game Manager Class
/// </summary>
public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChange;
    public Enums.GameState gameState { get; private set; }
    private static GameManager _instance = null;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {     
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Update()
    {
        //switch statement heres
        print(gameState);
    }

    public void SetGameState(Enums.GameState gameState)
    {
        this.gameState = gameState;
        if (OnStateChange != null)
        {
            OnStateChange(null, EventArgs.Empty);
        }
    }
}
