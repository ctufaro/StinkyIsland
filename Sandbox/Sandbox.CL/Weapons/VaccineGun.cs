using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    //Class Implementing Strategy Interface
    public class VaccineGun : IWeapon
    {
        public int DamageLevel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Engage()
        {
            Console.WriteLine("Shoots Projectile");
        }

        public void Disengage()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }
}
