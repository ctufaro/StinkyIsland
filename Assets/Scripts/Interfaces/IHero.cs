using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IHero
{
    void Attack(IWeapon weapon);
    void Damaged();
}
