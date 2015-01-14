using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    public class Weapon
    {
        private IWeapon _weapon;

        public Weapon(IWeapon weapon)
        {
            this._weapon = weapon;
        }

        public void Engage()
        {
            this._weapon.Engage();
        }

    }
}
