using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GasBlaster : AbstractWeapon
{
    private ParticleSystem[] gasBlasterSystem;
    private ParticleSystem gasBlaster;
    private Animator heroAnimator;
    private int direction;

    public void Awake()
    {
        gasBlasterSystem = this.GetComponentsInChildren<ParticleSystem>();
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
    }

    private void PointSpray()
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
        foreach (var spray in gasBlasterSystem)
        {
            spray.loop = true;
            spray.renderer.sortingLayerName = "Spray";
        }
    }
}

