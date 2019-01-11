using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro
{
    class Program
    {
        // Note we are now returning an int, rather than void.
        static int Main(string[] args)
        {
            // Display a message and wait for Enter key to be pressed.
            Console.WriteLine("***** My First C# App *****");
            Console.WriteLine("Hello World!");
            Console.WriteLine();
            // Console.ReadLine();
            // Return an arbitrary error code.
            return -1;
        }
    }
}
