using UnityEngine;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    private Hero hero;
    
    // Use this for initialization
	void Start () {
        hero = ScriptableObject.CreateInstance<Hero>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            hero.Attack(new GasBlaster());
        }

        if (Input.GetButton("Fire2"))
        {
            hero.Attack(new VaccineGun());
        }

	}
}
