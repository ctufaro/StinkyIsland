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

    public static event EventHandler OnGameObjectEnter;
    public static event EventHandler OnGameObjectExit;

	// Use this for initialization
	void Start () {
        fart = this.GetComponent<CircleCollider2D>();
        fartSprite = this.GetComponent<SpriteRenderer>();
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
        fartSprite.color = (currentFartTime % 2 == 0) ? Color.magenta : Color.green;
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
            fartSprite.color = Color.white;
        }
    }

    void Pop()
    {
        GameObject poof = Instantiate((Resources.Load("Poof")), new Vector3(this.transform.position.x, this.transform.position.y, -1f), Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(fartPop, this.transform.position);
        Destroy(this.gameObject);
        Destroy(poof, .8f);
    }




}
