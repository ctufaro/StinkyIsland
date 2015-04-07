using UnityEngine;
using System;
using System.Collections;

public class StenchieTransform : MonoBehaviour {

    public AudioClip transformSound;
    public event EventHandler OnTransform;
    private bool played = false;
    private bool transformed = false;
    
    
    // Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    void Transform()
    {
        if (!transformed)
        {
            string vType = (UnityEngine.Random.Range(1, 3) == 1) ? "A" : "B";
            GameObject go = Instantiate((Resources.Load("Villager" + vType)), this.transform.position, Quaternion.identity) as GameObject;
            StartCoroutine(FadeTo(this.transform, go.transform, 0f, .5f));
            Destroy(gameObject, 1f);
            transformed = true;

            if (OnTransform != null)
            {
                OnTransform(null, EventArgs.Empty);
            }

        }
    }

    void Magic()
    {
        GameObject blue = Instantiate((Resources.Load("BlueMagic")), new Vector3(this.transform.position.x,this.transform.position.y,-1f), Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(transformSound, this.transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Dart"))
        {
            Transform();
            Destroy(collision.gameObject);
        }

    }

    IEnumerator FadeTo(Transform stenchie, Transform villager, float aValue, float aTime)
    {
        float alpha, r, g, b = 0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alpha = 1f;
            r = villager.renderer.material.color.r;
            g = villager.renderer.material.color.g;
            b = villager.renderer.material.color.b;

            stenchie.renderer.material.SetFloat("_Alpha", Mathf.Lerp(1f, 0, t));
            villager.renderer.material.color = new Color(r, g, b, Mathf.Lerp(aValue, alpha, t));
            
            if (villager.renderer.material.color.a > .1f && !played)
            {
                Magic();
                played = true;
            }

            yield return null;
        }
    }
}
