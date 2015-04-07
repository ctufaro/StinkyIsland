using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GasBlaster : AbstractWeapon
{
    public AudioClip blastSound;
    private ParticleSystem[] gasBlasterSystem;
    private ParticleSystem gasBlaster;
    private CircleCollider2D gasCollider; 
    private Animator heroAnimator;    
    private int direction;
    private static bool empty;

    public static event EventHandler OnDisengage;
    public static event EventHandler OnEngage;

    public void Awake()
    {
        empty = false;
        gasBlasterSystem = this.GetComponentsInChildren<ParticleSystem>();
        gasCollider = this.GetComponentInChildren<CircleCollider2D>();
        heroAnimator = this.GetComponent<Animator>();
        gasBlaster = gasBlasterSystem[0];
        ToggleSpray(empty);
        ConfigureSpray();
    }

    public static void FillMeter(bool isActive)
    {
        empty = isActive;
    }

    public override string Name
    {
        get { return "GasBlaster"; }
    }

    public override void Engage(Vector3 position, Quaternion rotation)
    {
        //point spray
        PointSpray();
        
        //shoot is empty is false        
        ToggleSpray(empty);

        //engage collider
        gasCollider.enabled = empty;

        //notify subscribers weapon has been engaged
        if (OnEngage != null)
        {
            OnEngage(this.gameObject, EventArgs.Empty);
        }
    }

    private void PointSpray()
    {
        Quaternion rotation;
        Vector3 directionMotion = new Vector3(rigidbody2D.velocity.x * -1, rigidbody2D.velocity.y * -1, 0);

        //print(directionMotion);
        
        if (directionMotion != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(directionMotion);
            direction = heroAnimator.GetInteger("Direction");

            switch (direction)
            {
                case (0):
                case (1):
                    gasBlaster.transform.localPosition = new Vector3(-0.069f, 0.794f, 0f);
                    //rotation = Quaternion.LookRotation(-transform.up);
                    break;
                case (2):
                    gasBlaster.transform.localPosition = new Vector3(-0.31f, -0.212f, 0f);
                    //rotation = Quaternion.LookRotation(transform.right);
                    break;
                case (3):
                    gasBlaster.transform.localPosition = new Vector3(-0.053f, -0.341f, 0f);
                    //rotation = Quaternion.LookRotation(transform.up);
                    break;
                case (4):
                    gasBlaster.transform.localPosition = new Vector3(0.313f, -0.206f, 0f);
                    //rotation = Quaternion.LookRotation(-transform.right);
                    break;
                default:
                    //rotation = Quaternion.LookRotation(transform.forward);
                    break;

            }

            gasBlaster.transform.rotation = rotation;
        }
        else
        {
            //if our hero has stopped moving and is still facing a different direction, reset the Blast Directions
            //if(direction != heroAnimator.GetInteger("Direction"))
            //{
            //    ResetBlastDirection();
            //}
            ResetBlastDirection();
        }
        //print(heroAnimator.GetInteger("Direction"));
    }

    private void ResetBlastDirection()
    {
        Quaternion rotation;
        
        switch (heroAnimator.GetInteger("Direction"))
        {
            case (0):
            case (1):
                gasBlaster.transform.localPosition = new Vector3(-0.069f, 0.794f, 0f);
                rotation = Quaternion.LookRotation(-transform.up);
                break;
            case (2):
                gasBlaster.transform.localPosition = new Vector3(-0.31f, -0.212f, 0f);
                rotation = Quaternion.LookRotation(transform.right);
                break;
            case (3):
                gasBlaster.transform.localPosition = new Vector3(-0.053f, -0.341f, 0f);
                rotation = Quaternion.LookRotation(transform.up);
                break;
            case (4):
                gasBlaster.transform.localPosition = new Vector3(0.313f, -0.206f, 0f);
                rotation = Quaternion.LookRotation(-transform.right);
                break;
            default:
                rotation = Quaternion.LookRotation(transform.forward);
                break;

        }
        gasBlaster.transform.rotation = rotation;
    }

    public override void Disengage()
    {
        ToggleSpray(false);

        //disengage collider
        gasCollider.enabled = false;

        //notify subscribers weapon has been disengaged
        if (OnDisengage != null)
        {
            OnDisengage(this.gameObject, EventArgs.Empty);
        }
    }

    private void ToggleSpray(bool on)
    {
        foreach (var spray in gasBlasterSystem)
        {
            if (spray != null)
            {
                spray.enableEmission = on;
            }
        }
    }

    private void ConfigureSpray()
    {

        gasCollider.enabled = false;

        foreach (var spray in gasBlasterSystem)
        {
            spray.loop = true;
            spray.renderer.sortingLayerName = "Hero";
        }
    }
}

