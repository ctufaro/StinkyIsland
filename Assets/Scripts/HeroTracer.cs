using UnityEngine;
using System;
using System.Collections;

public class HeroTracer : MonoBehaviour {

    private Animator heroAnimator;
    public static event EventHandler OnAboutToFire;
    public bool traceOn;

	// Use this for initialization
	void Start () {
        heroAnimator = GetComponent<Animator>();
        traceOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 endPosition = Vector3.forward;
        Vector3 startPosition = this.gameObject.transform.position;
        Vector2 direction = Vector2.up;
        Enums.Direction enumDirection = Enums.Direction.Up;

        switch (heroAnimator.GetInteger("Direction"))
        {
            case (0):
            case (1):
                startPosition = new Vector3(transform.position.x - 0.063f, transform.position.y + 1.003f, 0f);
                endPosition = new Vector3(transform.position.x - 0.063f, transform.position.y + 10f, 0f);
                direction = Vector2.up;
                enumDirection = Enums.Direction.Up;
                break;
            case (2):
                startPosition = new Vector3(transform.position.x - 0.576f, transform.position.y - 0.205f, 0f);
                endPosition = new Vector3(transform.position.x - 10f, transform.position.y - 0.205f, 0f);
                direction = -Vector2.right;
                enumDirection = Enums.Direction.Left;
                break;
            case (3):
                startPosition = new Vector3(transform.position.x - 0.049f, transform.position.y - 0.8f, 0f);
                endPosition = new Vector3(transform.position.x - 0.063f, transform.position.y - 10f, 0f);
                direction = -Vector2.up;
                enumDirection = Enums.Direction.Down;
                break;
            case (4):
                startPosition = new Vector3(transform.position.x + 0.578f, transform.position.y - 0.213f, 0f);
                endPosition = new Vector3(transform.position.x + 10f, transform.position.y - 0.205f, 0f);
                direction = Vector2.right;
                enumDirection = Enums.Direction.Right;
                break;
            default:
                break;
        }

        RayTrace(startPosition, endPosition, direction, enumDirection);

	}

    void RayTrace(Vector2 startPosition, Vector2 endPosition, Vector2 direction, Enums.Direction enumDirection)
    {
        var hit = Physics2D.Raycast(new Vector2(startPosition.x, startPosition.y), direction);

        if (hit)
        {
            if (!hit.collider.tag.Equals("Player") && hit.collider.tag.Equals("Enemy"))
            {
                if (OnAboutToFire != null)
                {
                    OnAboutToFire(enumDirection, EventArgs.Empty);
                }
            }
        }

        if (traceOn)
        {
            Debug.DrawLine(startPosition, endPosition, Color.blue);
        }
    }
}
