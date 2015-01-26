using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator animator;
    private TouchStick tStick;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        tStick = CFInput.ctrl.GetStick("Walk");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement(CFInput.GetAxis("Horizontal"), CFInput.GetAxis("Vertical"));
    }

    void ManageMovement(float horizontal, float vertical)
    {
        print(string.Format("Horizontal:{0}, Vertical{1}", horizontal, vertical));

        if (animator)
        {
            if (tStick.GetFourWayDir() == TouchStick.StickDir.NEUTRAL)
            {
                animator.SetBool("Moving",false);
                rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                animator.SetBool("Moving", (horizontal != 0f || vertical != 0f));
                Vector3 movement = new Vector3(horizontal, vertical, 0);
                rigidbody2D.velocity = movement * speed;
            }

            switch (tStick.GetFourWayDir())
            {
                case(TouchStick.StickDir.U):
                    animator.SetInteger("Direction", 1);
                    break;
                case (TouchStick.StickDir.D):
                    animator.SetInteger("Direction", 3);
                    break;
                case (TouchStick.StickDir.L):
                    animator.SetInteger("Direction", 2);
                    break;
                case (TouchStick.StickDir.R):
                    animator.SetInteger("Direction", 4);
                    break;
                //case (TouchStick.StickDir.NEUTRAL):
                //    animator.SetBool("Moving", false);
                //    break;

            }

            //if ((vertical > horizontal) && (vertical > 0))
            //{
            //    animator.SetInteger("Direction", 1);
            //}

            //if ((vertical < horizontal) && (horizontal > 0))
            //{
            //    animator.SetInteger("Direction", 4);
            //}

            //if ((vertical < horizontal) && (vertical < 0))
            //{
            //    animator.SetInteger("Direction", 3);
            //}

            //if ((vertical > horizontal) && (horizontal < 0))
            //{
            //    animator.SetInteger("Direction", 2);
            //} 


        }
    }

}
