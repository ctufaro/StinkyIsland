using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VaccineGun : AbstractWeapon
{
    public override string Name
    {
        get { return "VaccineGun"; }
    }

    public override void Engage(Vector3 position, Quaternion rotation)
    {
        print("VaccineGun Engaged");
    }

    public override void Disengage()
    {
        print("VaccineGun Disengage");
    }
}