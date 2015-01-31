using UnityEngine;
using System.Collections;

public class MonsterBehavior : MonoBehaviour {

    private Monster monster;

    // Use this for initialization
    void Start()
    {
        monster = gameObject.AddComponent<Monster>();
        monster.Fart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

