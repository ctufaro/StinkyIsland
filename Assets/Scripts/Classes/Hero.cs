using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hero : ScriptableObject, IHero
{ 
    public void Attack(IWeapon weapon)
    {
        Debug.Log("Attacked with weapon " + weapon.Name);
    }

    public void Damaged()
    {
        //Debug.Log("Damaged");
    }
}
