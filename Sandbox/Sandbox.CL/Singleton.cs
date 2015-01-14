using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    public class Singleton
    {
        private static readonly Singleton _instance = new Singleton();

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private Singleton() { }
    }
}
