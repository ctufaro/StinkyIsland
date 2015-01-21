using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour, IWeapon
{
    public abstract string Name { get; }
    public abstract void Engage(Vector3 position, Quaternion rotation);
    public abstract void Disengage();
}
