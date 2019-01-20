using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public static class ProgramExit
    {
        public static void Exit()
        {
            Console.WriteLine("The program will be terminated. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
    }
}
