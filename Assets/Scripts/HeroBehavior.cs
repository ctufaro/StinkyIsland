using UnityEngine;
using System.Collections;

public delegate bool TouchKeyBoardHandler(string command);

public class HeroBehavior : MonoBehaviour {

    private Hero hero;
    private TouchKeyBoardHandler getButton;
    private TouchKeyBoardHandler getButtonUp;
    private TouchKeyBoardHandler getButtonDown;
    
    // Use this for initialization
	void Start () {
        hero = gameObject.AddComponent<Hero>();        

        #if UNITY_EDITOR
            ManageMovementKeyboard();
        #elif UNITY_WEBPLAYER
            ManageMovementKeyboard();
        #else
            ManageMovementTouch();
        #endif
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Button A Fire
        if (getButton("Fire1"))
        {
            hero.Attack(Enums.Button.A, this.transform.position, this.transform.rotation);
        }
        else if (getButtonUp("Fire1"))
        {
            hero.Shield();
        }
        #endregion

        #region Button B Fire
        if (getButtonDown("Fire2"))
        {
            hero.Attack(Enums.Button.B, this.transform.position, this.transform.rotation);
        }
        else if (getButtonUp("Fire2"))
        {
            hero.Shield();
        }
        #endregion
    }

    void ManageMovementTouch()
    {
        getButton = (x) => { return CFInput.GetButton(x); };
        getButtonUp = (x) => { return CFInput.GetButtonUp(x); };
        getButtonDown = (x) => { return CFInput.GetButtonDown(x); };
    }

    void ManageMovementKeyboard()
    {
        getButton = (x) => { return Input.GetButton(x); };
        getButtonUp = (x) => { return Input.GetButtonUp(x); };
        getButtonDown = (x) => { return Input.GetButtonDown(x); };
    }

}
