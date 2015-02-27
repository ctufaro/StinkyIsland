using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {
    int MonsterCounter = 0;
	// Use this for initialization
	void Start () {

        AddMonster(new Vector2(5.5f, 13.5f));
        AddMonster(new Vector2(-4.2f, 13.22f));
        //AddMonster(new Vector2(-4.2f, 5.47f));
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AddMonster(Vector2 position)
    {
        ++MonsterCounter;
        GameObject newMonster = Resources.Load("MonsterPrefab") as GameObject;
        newMonster.name = "Monster"+MonsterCounter.ToString();
        Rigidbody2D nm = Instantiate(newMonster.GetComponent<Rigidbody2D>(), position, Quaternion.identity) as Rigidbody2D;

        nm.renderer.sortingLayerName = "Monster";
    }
}
