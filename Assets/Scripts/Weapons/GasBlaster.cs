using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GasBlaster : AbstractWeapon
{
    public override string Name
    {
        get { return "GasBlaster"; }
    }

    public override void Engage(Vector3 position, Quaternion rotation)
    {
        print("GasBlaster Engage");
        //var ps = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        //foreach (var s in ps)
        //{
        //    s.enableEmission = true;
        //}
    }

    public override void Disengage()
    {
        print("GasBlaster Disengage");
    }
}

