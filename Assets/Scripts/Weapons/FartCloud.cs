using UnityEngine;
using System;
using System.Collections;

public class FartCloud : MonoBehaviour
{

    //events
    public static event EventHandler GreenGasserPopped;
    
    //member variables
    private ParticleSystem fart;
    private float fartTime = 10f;
    private float fartThreshold = -30f;
    private float currentFartTime;
    public ParticleSystem preFab;


    // Use this for initialization
    void Start()
    {
        //Hero.Disarm += new Hero.DisengageHandler(Hero_Disarm);
        fart = this.transform.GetComponentsInChildren<ParticleSystem>()[0];
    }

    //void Hero_Disarm(Collider2D col)
    //{
    //    OnTriggerExit2D(col);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (fart)
        {
            fart.startColor = Color.green;
            ResetFartTime();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        ShakeAndPop(coll);
    }

    void ResetFartTime()
    {
        currentFartTime = fartTime;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        ShakeAndPop(coll);
    }

    
    //TODO: Clean this shit up
    void ShakeAndPop(Collider2D coll)
    {
        if (!coll.name.Equals("Smoke Collider"))
        {
            return;
        }

        currentFartTime = currentFartTime - 1;
        fart.Clear();
        fart.startColor = (currentFartTime % 2 == 0) ? Color.magenta : Color.green;

        if (currentFartTime < fartThreshold)
        {
            if (preFab != null)
            {
                preFab.Play();

                if (GreenGasserPopped != null)
                {
                    //fire event
                    GreenGasserPopped(this, EventArgs.Empty);
                }

                preFab = null;
                fart.enableEmission = false;
                Destroy(this.gameObject, 2f);
            }
        }
    }
}
