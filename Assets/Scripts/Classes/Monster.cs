using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster : MonoBehaviour, IMonster
{

    public Enums.MonsterType Type { get; set; }
    public string Name { get; set; }

    public void Fart()
    {
        print("Monster FART!!");
    }

    public void Damaged()
    {
        //throw new NotImplementedException();
    }
}
