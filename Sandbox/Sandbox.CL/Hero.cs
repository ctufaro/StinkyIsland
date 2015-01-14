using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    public class Hero : IHero
    {
        public void Spray()
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void ReloadWeapon()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            Weapon A = new Weapon(new GasBlaster());
            Weapon B = new Weapon(new VaccineGun());
            A.Engage();
            B.Engage();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }
    }
}
