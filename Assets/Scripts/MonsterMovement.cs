using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour
{

    private Animator animator;
    private int firstDirection;
    private Monster monster;
<<<<<<< HEAD

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        monster = gameObject.AddComponent<Monster>();
    }


    // Update is called once per frame
    void Update()
    {
        if (animator)
        {
            if (monster.MonsterFart && !monster.FartStarted)
            {
                //no matter what I do, the fart gets skipped sometimes while the monster walks in place
                animator.StopPlayback();
                Fart();
            }
            else if (!monster.MonsterFart && !monster.FartStarted)
            {
                float horizontal = monster.CurrentX;
                float vertical = monster.CurrentY;
                //print(horizontal);
                //print(vertical);
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

                if (vertical == 0f && horizontal == 0f)
                    animator.StopPlayback();

                animator.SetBool("Moving", (horizontal != 0f || vertical != 0f));
                Vector3 movement = new Vector3(horizontal, vertical, 0);
                rigidbody2D.velocity = movement * monster.Speed;
            }
        }
=======
    
    // Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        monster = gameObject.AddComponent<Monster>();
	}

	
	// Update is called once per frame
	void Update()
    {
        if (animator)
        {
            if (monster.Collided)
            {
                animator.SetBool("Moving", false);
                monster.Collided = false;
            }
            else
            {
                if (monster.MonsterFart && !monster.FartStarted)
                {
                    //no matter what I do, the fart gets skipped sometimes while the monster walks in place
                    animator.StopPlayback();
                    Fart();
                }
                else if (!monster.MonsterFart && !monster.FartStarted)
                {
                    float horizontal = monster.CurrentX;
                    float vertical = monster.CurrentY;
                    //print(horizontal);
                    //print(vertical);
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

                    if (vertical == 0f && horizontal == 0f)
                        animator.StopPlayback();

                    animator.SetBool("Moving", (horizontal != 0f || vertical != 0f));
                    Vector3 movement = new Vector3(horizontal, vertical, 0);
                    rigidbody2D.velocity = movement * monster.Speed;
                }
            }
        }        
>>>>>>> 9e903a2ac50c84f7657d086fc72f79f07ab6159f
    }

    void Fart()
    {

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
        //why doesn't this work every time? it completely misses the condition for the transition
        //animator.StopPlayback();
        monster.FartStarted = true;
        animator.SetBool("Moving", false);
        //let the animation finish

        yield return new WaitForSeconds(1);
        animator.SetTrigger("Fart");
        firstDirection = animator.GetInteger("Direction");
        animator.SetInteger("Direction", 0);
<<<<<<< HEAD



        yield return new WaitForSeconds(1);
        monster.MonsterFart = false;
        monster.FartStarted = false;
        animator.SetInteger("Direction", firstDirection);
    }


}
=======



        yield return new WaitForSeconds(1);
        monster.MonsterFart = false;
        monster.FartStarted = false;
        animator.SetInteger("Direction", firstDirection);
    }

    
}
>>>>>>> 9e903a2ac50c84f7657d086fc72f79f07ab6159f
