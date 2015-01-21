using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IWeapon
{
    string Name { get; }
    void Engage(Vector3 position, Quaternion rotation);
    void Disengage();
}
