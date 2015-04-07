using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StenchieBehavior : MonoBehaviour
{

    public float speed = 1.0f;
    public AudioClip[] fartSound;
    private Animator animator;
    private StenchieMovement movement;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<StenchieMovement>();
        StartCoroutine(RandomFart());
        //HeroTracer.OnAboutToFire += new System.EventHandler(HeroTracer_OnAboutToFire);
    }

    void HeroTracer_OnAboutToFire(object sender, System.EventArgs e)
    {
        Enums.Direction direction = (Enums.Direction)sender;
        
        switch (direction)
        {
            case (Enums.Direction.Up):
                FartUp();
                break;
            case (Enums.Direction.Left):
                FartLeft();
                break;
            case (Enums.Direction.Down):
                FartDown();
                break;
            case (Enums.Direction.Right):
                FartRight();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    #region Fart Methods

    IEnumerator RandomFart()
    {
        while (true)
        {
            
            int wait = Random.Range(1, 5);
            yield return new WaitForSeconds(wait);
            animator.SetBool("Moving", false);
            animator.SetBool("Farting", true);
            movement.Stop();
            yield return new WaitForSeconds(.5f);
            animator.SetBool("Farting", false);
            animator.SetBool("Moving", true);
            movement.ChangeAxis();
        }
    }
    
    void FartLeft()
    {
        StartCoroutine(FartAsynch("Monster", 0, 1.15f, .1f));
    }

    void FartRight()
    {
        StartCoroutine(FartAsynch("Monster",0, -1.3f, -.2f));  
    }

    void FartDown()
    {
        StartCoroutine(FartAsynch("Monster", 0, 0f, 1.2f));
    }

    void FartUp()
    {
        StartCoroutine(FartAsynch("Monster", 1, 0f, 0f));
    }

    IEnumerator FartAsynch(string layer, int layerOrder, float newX, float newY)
    {
        yield return new WaitForSeconds(0f);

        GameObject instant = (GameObject)Instantiate(Resources.Load("FartLinger") as GameObject);
        instant.renderer.sortingLayerName = layer;
        instant.renderer.sortingOrder = layerOrder;

        int s = Random.Range(0, 3);
        AudioSource.PlayClipAtPoint(fartSound[s], Vector3.zero);

        StartCoroutine(FadeTo(instant.transform, 1.0f, 0.3f, newX, newY));
        yield return null;
    }

    IEnumerator FadeTo(Transform tran, float aValue, float aTime, float newX, float newY)
    {
        float alpha, x, y, r, g, b = 0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            alpha = 0f;
            x = this.transform.localPosition.x;
            y = this.transform.localPosition.y;
            r = tran.renderer.material.color.r;
            g = tran.renderer.material.color.g;
            b = tran.renderer.material.color.b;

            tran.renderer.material.color = new Color(r, g, b, Mathf.Lerp(alpha, aValue, t));
            tran.localPosition = new Vector3(Mathf.Lerp(x, x + newX, t), Mathf.Lerp(y + newY, y + newY, t), 0f);
            tran.localScale = new Vector3(Mathf.Lerp(.5f, 1f, t), Mathf.Lerp(.5f, 1f, t), 0f);
            yield return null;
        }
    }

    #endregion
}
