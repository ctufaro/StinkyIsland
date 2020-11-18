using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GasBlaster : AbstractWeapon
{
    private ParticleSystem[] gasBlasterSystem;
    private ParticleSystem gasBlaster;
    private CircleCollider2D gasCollider; 
    private Animator heroAnimator;    
    private int direction;

    public void Awake()
    {
        gasBlasterSystem = this.GetComponentsInChildren<ParticleSystem>();
        gasCollider = this.GetComponentInChildren<CircleCollider2D>();
        heroAnimator = this.GetComponent<Animator>();
        gasBlaster = gasBlasterSystem[0];
        ToggleSpray(false);
        ConfigureSpray();
    }
    
    
    public override string Name
    {
        get { return "GasBlaster"; }
    }

    public override void Engage(Vector3 position, Quaternion rotation)
    {
        //point spray
        PointSpray();
        
        //shoot spray
        ToggleSpray(true);

        //engage collider
        gasCollider.enabled = true;
    }

    private void PointSpray()
    {
        Quaternion rotation;
        Vector3 directionMotion = new Vector3(GetComponent<Rigidbody2D>().velocity.x * -1, GetComponent<Rigidbody2D>().velocity.y * -1, 0);
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
            if(direction != heroAnimator.GetInteger("Direction"))
            {
                ResetBlastDirection();
            }
        }
        
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
            spray.GetComponent<Renderer>().sortingLayerName = "Spray";
        }
    }
}

