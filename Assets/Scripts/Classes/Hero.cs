using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hero : MonoBehaviour, IHero
{
    private List<AbstractWeapon> weapons;
    private AbstractWeapon weapon;

    public void Start()
    {
        weapons = new List<AbstractWeapon>
        {
            gameObject.AddComponent<GasBlaster>(),
            gameObject.AddComponent<VaccineGun>()
        };        
    }
    
    public void Attack(Enums.Button button, Vector3 position, Quaternion rotation)
    {
        switch (button)
        {
            case(Enums.Button.A):
            {
                weapon = weapons[0];
                break;    
            }

            case (Enums.Button.B):
            {
                weapon = weapons[1];
                break;
            }

            default:
                weapon = weapons[0];
                break;
        }

        weapon.Engage(position, rotation);
    }

    public void Damaged()
    {
        //Debug.Log("Damaged");
    }

    public void Shield()
    {
        throw new NotImplementedException();
    }
}
