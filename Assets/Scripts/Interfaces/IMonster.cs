using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IMonster
{
    Enums.MonsterType Type { get; set; }
    string Name { get; set; }
    void Fart();
    void Damaged();
}
