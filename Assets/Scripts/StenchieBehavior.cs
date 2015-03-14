using UnityEngine;
using System.Collections;

public class StenchieBehavior : MonoBehaviour {

    public float speed = 1.0f;
    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", 3);
	}
	
	// Update is called once per frame
	void Update () {
    
    }

    void ManageMovementKeyboard()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (animator)
        {
            if ((vertical > horizontal) && (vertical > 0))
            {
                animator.SetInteger("Direction", 1);
            }

            if ((vertical < horizontal) && (horizontal > 0))
            {
                animator.SetInteger("Direction", 4);
            }

            if ((vertical < horizontal) && (vertical < 0))
            {
                animator.SetInteger("Direction", 3);
            }

            if ((vertical > horizontal) && (horizontal < 0))
            {
                animator.SetInteger("Direction", 2);
            }

            animator.SetBool("Moving", (horizontal != 0f || vertical != 0f));
            Vector3 movement = new Vector3(horizontal, vertical, 0);
            rigidbody2D.velocity = movement * speed;
        }
    }

    void Fart()
    {
        StartCoroutine("FireFart");
    }

    IEnumerator FireFart()
    {
        animator.SetBool("Farting", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Farting", false);
    }

    void FartLeft()
    {
        StartCoroutine(FartAsynch("Monster",0, 1.15f, .5f));     
    }

    void FartRight()
    {
        StartCoroutine(FartAsynch("Monster",0, -1.15f, .5f));  
    }

    void FartDown() 
    {
        StartCoroutine(FartAsynch("Monster",0, 0f, 1.2f)); 
    }

    void FartUp() 
    {
        StartCoroutine(FartAsynch("Monster",1, 0f, 0f)); 
    }

    IEnumerator FartAsynch(string layer, int layerOrder, float newX, float newY)
    {
        yield return new WaitForSeconds(0f);

        GameObject instant = (GameObject)Instantiate(Resources.Load("FartLinger") as GameObject);
        instant.renderer.sortingLayerName = layer;
        instant.renderer.sortingOrder = layerOrder;
        StartCoroutine(FadeTo(instant.transform, 1.0f, 0.3f, newX, newY));        
        
        yield return null;
    }

    IEnumerator FadeTo(Transform tran, float aValue, float aTime, float newX, float newY)
    {
        float alpha,x,y,r,g,b = 0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alpha = 0f;
            x = this.transform.localPosition.x;
            y = this.transform.localPosition.y;
            r = tran.renderer.material.color.r;
            g = tran.renderer.material.color.g;
            b = tran.renderer.material.color.b;

            tran.renderer.material.color = new Color(r, g, b, Mathf.Lerp(alpha, aValue, t));
            tran.localPosition = new Vector3(Mathf.Lerp(x, x + newX, t), Mathf.Lerp(y, y + newY, t), 0f);
            tran.localScale = new Vector3(Mathf.Lerp(.5f, 1f, t), Mathf.Lerp(.5f, 1f, t), 0f); ;
            yield return null;
        }
    }
}
