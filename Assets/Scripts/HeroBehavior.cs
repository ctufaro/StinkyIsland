using UnityEngine;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    private Hero hero;
    
    // Use this for initialization
	void Start () {
        hero = gameObject.AddComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region Button A Fire
        if (CFInput.GetButton("Fire1"))
        {
            hero.Attack(Enums.Button.A, this.transform.position, this.transform.rotation);
        }
        else if (CFInput.GetButtonUp("Fire1"))
        {
            hero.Shield();
        }
        #endregion

        #region Button B Fire
        if (CFInput.GetButtonDown("Fire2"))
        {
            hero.Attack(Enums.Button.B, this.transform.position, this.transform.rotation);
        }
        else if (CFInput.GetButtonUp("Fire2"))
        {
            hero.Shield();
        }
        #endregion
    }
}
