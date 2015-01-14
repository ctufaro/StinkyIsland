using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    //Strategy Interface
    public interface IWeapon
    {
        int DamageLevel { get; set; }
        //void SetStlye();
        void Engage();
        void Disengage();
        void Reload();        
    
    }
}
