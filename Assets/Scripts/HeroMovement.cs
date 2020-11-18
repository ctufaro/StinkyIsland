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
    void Update()
    {
        #if UNITY_EDITOR
            ManageMovementKeyboard();
        #elif UNITY_WEBPLAYER
            ManageMovementKeyboard();
        #else
            ManageMovementTouch();
        #endif
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
            GetComponent<Rigidbody2D>().velocity = movement * speed;
        }
    }

    void ManageMovementTouch()
    {
        float horizontal = CFInput.GetAxis("Horizontal");
        float vertical = CFInput.GetAxis("Vertical");        
        
        if (animator)
        {
            if (tStick.GetFourWayDir() == TouchStick.StickDir.NEUTRAL)
            {
                animator.SetBool("Moving",false);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else
            {
                animator.SetBool("Moving", (horizontal != 0f || vertical != 0f));
                Vector3 movement = new Vector3(horizontal, vertical, 0);
                GetComponent<Rigidbody2D>().velocity = movement * speed;
            }

            switch (tStick.GetFourWayDir())
            {
                case (TouchStick.StickDir.UL):
                case (TouchStick.StickDir.UR):
                case (TouchStick.StickDir.U):
                    animator.SetInteger("Direction", 1);
                    break;
                case (TouchStick.StickDir.DL):
                case (TouchStick.StickDir.DR):
                case (TouchStick.StickDir.D):
                    animator.SetInteger("Direction", 3);
                    break;
                case (TouchStick.StickDir.L):
                    animator.SetInteger("Direction", 2);
                    break;
                case (TouchStick.StickDir.R):
                    animator.SetInteger("Direction", 4);
                    break;
            }
        }
    }

}
