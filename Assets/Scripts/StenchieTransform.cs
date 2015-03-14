using UnityEngine;
using System.Collections;

public class StenchieTransform : MonoBehaviour {

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
            string vType = (Random.Range(1, 3) == 1) ? "A" : "B";
            GameObject go = Instantiate((Resources.Load("Villager" + vType)), this.transform.position, Quaternion.identity) as GameObject;
            StartCoroutine(FadeTo(this.transform, go.transform, 0f, .5f));
            Destroy(gameObject, 1f);
            transformed = true;
        }
    }

    void Magic()
    {
        GameObject blue = Instantiate((Resources.Load("BlueMagic")), new Vector3(this.transform.position.x,this.transform.position.y,-1f), Quaternion.identity) as GameObject;
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
            r = stenchie.renderer.material.color.r;
            g = stenchie.renderer.material.color.g;
            b = stenchie.renderer.material.color.b;

            stenchie.renderer.material.color = new Color(r, g, b, Mathf.Lerp(alpha, aValue, t));
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
