using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/// <summary>
/// Singleton Game Manager Class
/// </summary>
public class GameManager : MonoBehaviour
{    
    public event EventHandler OnStateChange;
    public Enums.GameState gameState { get; private set; }
    public Enums.GameState stateOverride;
    private static GameManager _instance = null;
    private int greenGasPopCount = 0;    
    private int gemStoneCount = 0;
    private float healthSliderMeterAmount = 100;
    private float blasterSliderMeterAmount = 0;
    private float vaccineSliderMeterAmount = 0;
    
    //parameterized
    private int blueBerryIncreaseAmount = 50;
    private int purpleBerryIncreaseAmount = 50;
    private float gasDegradeRate = 0;//.15f;
    private float dartDegradeRate = 0;//8f;
    private int healthDegradeRate = 10;

    //HUD Elements
    private Text ScoreText;
    private Text StinkText;
    private Text ScreenText;
    private Text GemstoneText;
    private Slider StinkSlider;
    private Slider HealthSlider;
    private Slider BlasterSlider;
    private Slider VaccineSlider;
    private Image ScreenImage;
    
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

        //For Testing Purposes
        if (stateOverride != null)
        {
            this.gameState = stateOverride;
        }

        FartCollider.OnFartPop += new EventHandler(FartCollider_OnFartPop);
        FartCollider.OnGameObjectEnter += new EventHandler(FartCollider_OnGameObjectEnter);
        Collectible.OnItemPickUp += new EventHandler(Collectible_OnItemPickUp);
        GasBlaster.OnEngage += new EventHandler(GasBlaster_OnEngage);
        VaccineGun.OnEngage += new EventHandler(VaccineGun_OnEngage);
    }

    
    #region Events
    void VaccineGun_OnEngage(object sender, EventArgs e)
    {
        vaccineSliderMeterAmount = vaccineSliderMeterAmount - dartDegradeRate;
        if (vaccineSliderMeterAmount <= 0)
        {
            VaccineGun.FillMeter(false);
        }
    }

    void GasBlaster_OnEngage(object sender, EventArgs e)
    {
        blasterSliderMeterAmount = blasterSliderMeterAmount - gasDegradeRate;
        if (blasterSliderMeterAmount <= 0)
        {
            GasBlaster.FillMeter(false);
        }
    }

    void Collectible_OnItemPickUp(object sender, EventArgs e)
    {
        if (sender is GameObject)
        {
            GameObject go = sender as GameObject;
            switch (go.name)
            {
                case("GemStone"):
                    gemStoneCount++;
                    break;
                case("BlueBerry"):
                    blasterSliderMeterAmount = blasterSliderMeterAmount + blueBerryIncreaseAmount;
                    GasBlaster.FillMeter(true);
                    break;
                case("PurpleBerry"):
                    vaccineSliderMeterAmount = vaccineSliderMeterAmount + purpleBerryIncreaseAmount;
                    VaccineGun.FillMeter(true);
                    break;
                case("Heart"):
                    healthSliderMeterAmount = 100;
                    break;
                default:
                    break;
            }
        }
    }

    void FartCollider_OnGameObjectEnter(object sender, EventArgs e)
    {
        if (sender is Collider2D)
        {
            string tag = ((Collider2D)sender).tag;
            if (tag.Equals("Player"))
            {
                PlayerDamage();
            }
        }
    }

    void FartCollider_OnFartPop(object sender, EventArgs e)
    {
        greenGasPopCount++;
    }
    #endregion

    void Update()
    {        
        switch (gameState)
        {
            case(Enums.GameState.Splash):
                //print(gameState);
                break;
            case(Enums.GameState.MainMenu):
                //print(gameState);
                break;
            case(Enums.GameState.GetReady):
                FadeInto();
                break;
            case (Enums.GameState.LevelRunning):
                LevelRunning();
                break;
            case (Enums.GameState.LevelComplete):
                LevelComplete();
                break;
            case (Enums.GameState.LostLife):
                PlayerDeath();
                break;
            case (Enums.GameState.LevelReset):
                Reset();
                _instance.gameState = Enums.GameState.LevelRunning;
                LevelRunning();
                break;
            default:
                //SetGameState(Enums.GameState.LevelRunning);
                //print("Let's Go!!");
                break;
        }
    }

    IEnumerator ScreenFlash(float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            ScreenImage.color = Color.red;
            ScreenImage.color = new Color(ScreenImage.color.r, ScreenImage.color.g, ScreenImage.color.b, Mathf.Lerp(.8f, 0f, t));
            yield return null;
        }
    }

    void PlayerDamage()
    {        
        if (ScreenImage != null)
        {
            //take damage
            healthSliderMeterAmount = healthSliderMeterAmount - healthDegradeRate;
            StartCoroutine(ScreenFlash(.3f));     
        }
    }

    void PlayerDeath()
    {
        ScreenText.text = "You're Dead!";
        ScreenText.color = Color.white;
    }

    public void SetGameState(Enums.GameState gameState)
    {
        this.gameState = gameState;
        if (OnStateChange != null)
        {
            OnStateChange(null, EventArgs.Empty);
        }
    }

    //MAYBE KILLING RESOURCES    
    public void LevelRunning()
    {
        StinkText = GameObject.FindGameObjectWithTag("StinkText").GetComponent<Text>();
        ScreenText = GameObject.FindGameObjectWithTag("ScreenText").GetComponent<Text>();
        GemstoneText = GameObject.FindGameObjectWithTag("GemstoneText").GetComponent<Text>();
        StinkSlider = GameObject.FindGameObjectWithTag("StinkSlider").GetComponent<Slider>();
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        ScreenImage = GameObject.FindGameObjectWithTag("ScreenImage").GetComponent<Image>();
        HealthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        BlasterSlider = GameObject.FindGameObjectWithTag("BlasterSlider").GetComponent<Slider>();
        VaccineSlider = GameObject.FindGameObjectWithTag("VaccineSlider").GetComponent<Slider>();

        //lots of things to do here
        
        //Count prefabs of farts
        StinkText.text = GameObject.FindGameObjectsWithTag("GreenGasser").Length.ToString();
        
        //Increment/Decrement Stink Meter
        StinkSlider.value = Int32.Parse(StinkText.text);

        //Increment Score on green farts popping
        ScoreText.text = "Score: " + (greenGasPopCount * 100).ToString();

        //Record player damage
        HealthSlider.value = healthSliderMeterAmount;

        //Recording gemstones
        GemstoneText.text = gemStoneCount.ToString();

        //Set Blaster Slider
        BlasterSlider.value = blasterSliderMeterAmount;

        //Set VaccineSlider
        VaccineSlider.value = vaccineSliderMeterAmount;
            
        //Check for Hero Death
        if (healthSliderMeterAmount == 0){SetGameState(Enums.GameState.LostLife);}

    }

    public void LevelComplete()
    {
        ScreenText.text = @"Level Done!";
        ScreenText.color = Color.white;
    }

    public void FadeInto()
    {
        ScreenText = GameObject.FindGameObjectWithTag("ScreenText").GetComponent<Text>();
        ScreenImage = GameObject.FindGameObjectWithTag("ScreenImage").GetComponent<Image>();
        ScreenText.text = "Level 1 Demo";
        ScreenImage.color = Color.black;
        StartCoroutine(FadeToStart(ScreenImage, ScreenText, 2f));        
    }

    IEnumerator FadeToStart(Image p, Text txt, float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            p.color = new Color(p.color.r, p.color.g, p.color.b, Mathf.Lerp(1f, 0f, t));
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(2f, 0f, t));
            yield return null;
        }
        _instance.gameState = Enums.GameState.LevelRunning;
        yield return null;
    }
    
    public void Reset()
    {
        blasterSliderMeterAmount = 0f;
        vaccineSliderMeterAmount = 0f;
        healthSliderMeterAmount = 100f;
        gemStoneCount = 0;
        greenGasPopCount = 0;
    }

}
