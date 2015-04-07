using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {
    //int MonsterCounter = 0;
	// Use this for initialization

    public int stenchieCount = 4;
    private int count = 0;
    private StenchieTransform stenchieTransform;

	void Start () {

        //AddMonster(new Vector2(5.5f, 13.5f));
        //AddMonster(new Vector2(-4.2f, 13.22f));
        ////AddMonster(new Vector2(-4.2f, 5.47f));
        foreach (var stenchie in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            StenchieTransform transForm = stenchie.GetComponent<StenchieTransform>();
            transForm.OnTransform += new System.EventHandler(transForm_OnTransform);
        }
	}

    void transForm_OnTransform(object sender, System.EventArgs e)
    {
        count++;
        if (count == stenchieCount)
        {
            GameManager.instance.SetGameState(Enums.GameState.LevelComplete);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //void AddMonster(Vector2 position)
    //{
    //    ++MonsterCounter;
    //    GameObject newMonster = Resources.Load("MonsterPrefab") as GameObject;
    //    newMonster.name = "Monster"+MonsterCounter.ToString();
    //    Rigidbody2D nm = Instantiate(newMonster.GetComponent<Rigidbody2D>(), position, Quaternion.identity) as Rigidbody2D;

    //    nm.renderer.sortingLayerName = "Monster";
    //}
}
