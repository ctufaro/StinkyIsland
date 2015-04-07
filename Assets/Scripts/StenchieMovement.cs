using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void MoveHandler();

public class StenchieMovement : MonoBehaviour {

    private Rigidbody2D rigidB2D;
    private Animator animator;
    public float speed;
    public float changeInterval;
    private MoveHandler moveHandler;
    private int currentDirection;
    private List<float> collisionTimes;
    
    // Use this for initialization
	void Start () {
        collisionTimes = new List<float>();
        rigidB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Moving", true);
        
        //start moving
        Move(Random.Range(1, 4));

        //start coroutine
        StartCoroutine(NewRoute());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        moveHandler();
	}

    void Move(int direction)
    {
        this.currentDirection = direction;
        animator.SetInteger("Direction", direction);

        switch (direction)
        {
            case(1):
                moveHandler = () => { MoveUp(); }; 
                break;
            case(2):
                moveHandler = () => { MoveLeft(); }; 
                break;
            case(3):
                moveHandler = () => { MoveDown(); }; 
                break;
            case(4):
                moveHandler = () => { MoveRight(); }; 
                break;
        }
    }

    void MoveUp()
    {
        rigidB2D.velocity = Vector2.up * speed; 
    }

    void MoveDown()
    {
        rigidB2D.velocity = -Vector2.up * speed;
    }

    void MoveLeft()
    {
        rigidB2D.velocity = -Vector2.right * speed;
    }

    void MoveRight()
    {
        rigidB2D.velocity = Vector2.right * speed;
    }

    public void Stop()
    {
        moveHandler = () => { rigidB2D.velocity = Vector2.zero; };
    }



    public void ChangeAxis()
    {
        int[] yAxis = new int[] { 1, 3 };
        int[] xAxis = new int[] { 2, 4 };

        if (currentDirection == 1 || currentDirection == 3)
        {
            Move(xAxis[Random.Range(0, 1)]);
        }
        else
        {
            Move(yAxis[Random.Range(0, 1)]);
        }
    }

    IEnumerator NewRoute()
    {
        while (true)
        {
            ChangeAxis();
            yield return new WaitForSeconds(changeInterval);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionTimes.Add(Time.time);
        
        if (collision.gameObject.tag.Equals("Dart"))
        {
            Stop();
            return;
        }        

        if (collisionTimes.Count > 1)
        {
            float difference = (collisionTimes[collisionTimes.Count - 1] - collisionTimes[collisionTimes.Count - 2]);
            if (difference < 1.3)
            {
                ChangeAxis();
                return;
            }
        }
        
        switch (currentDirection)
        {
            case (1):
                Move(3);
                break;
            case (2):
                Move(4);
                break;
            case (3):
                Move(1);
                break;
            case (4):
                Move(2);
                break;
        }
    }
    
}
