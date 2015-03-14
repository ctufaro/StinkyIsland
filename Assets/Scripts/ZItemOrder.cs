using UnityEngine;
using System.Collections;

public class ZItemOrder : MonoBehaviour {

    private SpriteRenderer tempRend;
    private string sortingLayer;
    private int sortingOrder;
    
    //void Awake()
    //{
    //    tempRend = this.gameObject.GetComponent<SpriteRenderer>();
    //}
	
    
    // Use this for initialization
	void Start () {
        tempRend = this.gameObject.GetComponent<SpriteRenderer>();
        sortingLayer = tempRend.sortingLayerName;
        sortingOrder = tempRend.sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit2D(Collider2D coll)
    {
        this.tempRend.sortingLayerName = sortingLayer;
        this.tempRend.sortingOrder = sortingOrder;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //if somethine enters the trigger, change the sorting layer
        this.tempRend.sortingLayerName = "Hero";
        this.tempRend.sortingOrder = 100;
    }

    //void LateUpdate()
    //{
    //    tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint(tempRend.bounds.min).y * -1;
    //}
}
