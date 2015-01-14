using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sandbox.CL;

namespace Sandbox.CON
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton instance = Singleton.Instance;
            Console.WriteLine(instance == null);
            Console.ReadLine();
        }
    }
}
