using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void ManageMovement(float horizontal, float vertical)
    {
        if (animator)
        {
            animator.SetBool("Moving", (horizontal != 0f || vertical != 0f));
            
            if (vertical > 0)
            {
                animator.SetInteger("Direction", 1);
            }

            if (horizontal > 0)
            {
                animator.SetInteger("Direction", 4);
            }

            if (vertical < 0)
            {
                animator.SetInteger("Direction", 3);
            }

            if (horizontal < 0)
            {
                animator.SetInteger("Direction", 2);
            }

            Vector3 movement = new Vector3(horizontal, vertical, 0);
            rigidbody2D.velocity = movement * speed;
        }
    }

}
