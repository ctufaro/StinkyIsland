using System;
using UnityEngine;
using System.Collections;

public class FartCollider : MonoBehaviour {

    CircleCollider2D fart;
    SpriteRenderer fartSprite;
    private float fartTime = 10f;
    public float fartThreshold;
    public AudioClip fartPop;
    private float currentFartTime;
    private Color currentFartColor;

    public static event EventHandler OnGameObjectEnter;
    public static event EventHandler OnGameObjectExit;
    public static event EventHandler OnFartPop;

	// Use this for initialization
	void Start () {
        fart = this.GetComponent<CircleCollider2D>();
        fartSprite = this.GetComponent<SpriteRenderer>();
        currentFartColor = new Color(.423f, .807f, .474f, 1);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag.Equals("Weapon"))
        {
            Reset();
        }

        if (OnGameObjectExit != null)
        {
            OnGameObjectExit(coll, EventArgs.Empty);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("Weapon"))
        {
            Attacked();
            GasBlaster.OnDisengage += new System.EventHandler(GasBlaster_OnDisengage);
        }
        else if (coll.tag.Equals("Dart"))
        {
            //farts kill darts            
            GameObject softStar = Resources.Load("SoftStar") as GameObject;
            softStar.renderer.sortingLayerName = "Latest";
            Instantiate(softStar, new Vector3(coll.gameObject.transform.position.x, coll.gameObject.transform.position.y, 0f), Quaternion.identity);
            Destroy(coll.gameObject);
        }

        if (OnGameObjectEnter != null)
        {
            OnGameObjectEnter(coll, EventArgs.Empty);
        }
    }

    void GasBlaster_OnDisengage(object sender, System.EventArgs e)
    {
        Reset();
        GasBlaster.OnDisengage -= new System.EventHandler(GasBlaster_OnDisengage);
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag.Equals("Weapon"))
        {
            Attacked();
            GasBlaster.OnDisengage += new System.EventHandler(GasBlaster_OnDisengage);
        }

    }

    void Attacked()
    {
        currentFartTime = currentFartTime - 1;
        fartSprite.color = (currentFartTime % 2 == 0) ? Color.magenta : currentFartColor;
        if (Mathf.Abs(currentFartTime) > fartThreshold)
        {
            Pop();
        }
    }

    void Reset()
    {
        if (fartSprite)
        {
            currentFartTime = fartTime;
            fartSprite.color = currentFartColor;
        }
    }

    void Pop()
    {
        GameObject poof = Instantiate((Resources.Load("Poof")), new Vector3(this.transform.position.x, this.transform.position.y, -1f), Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(fartPop, this.transform.position);
        Destroy(this.gameObject);
        Destroy(poof, .8f);
        if (OnFartPop != null)
        {
            OnFartPop(null, EventArgs.Empty);
        }
    }




}
