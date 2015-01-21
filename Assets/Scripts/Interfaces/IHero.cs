using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IHero
{
    void Attack(Enums.Button button, Vector3 postion, Quaternion rotation);
    void Shield();
    void Damaged();
}
