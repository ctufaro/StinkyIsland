using UnityEngine;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    private Hero hero;
    
    // Use this for initialization
	void Start () {
        hero = gameObject.AddComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            hero.Attack(Enums.Button.A, this.transform.position, this.transform.rotation);
        }

        if (Input.GetButton("Fire2"))
        {
            hero.Attack(Enums.Button.B, this.transform.position, this.transform.rotation);
        }

	}
}
