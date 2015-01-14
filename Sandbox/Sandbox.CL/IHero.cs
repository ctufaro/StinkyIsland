using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    public interface IHero
    {
        void Spray();
        void Shoot();
        void ReloadWeapon();
        void Attack();
        void Die();
    }
}
