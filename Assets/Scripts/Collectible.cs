using UnityEngine;
using System;
using System.Collections;

public class Collectible : MonoBehaviour {

    public AudioClip pickUpSound;
    public static event EventHandler OnItemPickUp;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("Player"))
        {
            if (this.gameObject)
            {
                AudioSource.PlayClipAtPoint(pickUpSound, this.transform.position);
                Destroy(this.gameObject);

                if (OnItemPickUp != null)
                {
                    OnItemPickUp(this.gameObject, EventArgs.Empty);
                }

            }
        }
    }
}
