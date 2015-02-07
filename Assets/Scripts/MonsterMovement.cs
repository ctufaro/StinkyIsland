using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

    private Animator animator;
    public float speed = 1.0f;
    private int firstDirection;
    
    // Use this for initialization
	void Start () {
	    animator = GetComponent<Animator>();
        StartCoroutine("Bummy");
	}
	
	// Update is called once per frame
	void Update()
    {
        //ManageMovementKeyboard();        

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            animator.SetTrigger("Fart");
            firstDirection = animator.GetInteger("Direction");
            animator.SetInteger("Direction", 0);
            StartCoroutine("Dummy");
        }
    }

    void Fart()
    {
        animator.SetTrigger("Fart");
        firstDirection = animator.GetInteger("Direction");
        animator.SetInteger("Direction", 0);
        StartCoroutine("Dummy");
    }

    IEnumerator Bummy()
    {
        while (true)
        {
            int number = Random.Range(1, 5);
            animator.SetInteger("Direction", number);
            Fart();
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator Dummy()
    {
        yield return new WaitForSeconds(1);
        animator.SetInteger("Direction", firstDirection);
    }

    void ManageMovementKeyboard()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //float horizontal = this.transform.localPosition.x;
        //float vertical = this.transform.localPosition.y;

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
}
