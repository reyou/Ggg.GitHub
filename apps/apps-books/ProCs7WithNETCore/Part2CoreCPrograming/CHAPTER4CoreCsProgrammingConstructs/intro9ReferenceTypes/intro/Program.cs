using System;
using System.Drawing;

namespace intro
{ // Classes are always reference types.
    class Program
    {
        static void Main(string[] args)
        {
            ValueTypeAssignment();
            Console.ReadLine();
        }
        // Assigning two intrinsic value types results in
        // two independent variables on the stack.
        static void ValueTypeAssignment()
        {
            Console.WriteLine("Assigning value types\n");
            PointRef p1 = new PointRef(10, 10);
            PointRef p2 = p1;
            // Print both points.
            p1.Display();
            p2.Display();
            // Change p1.X and print again. p2.X is not changed.
            p1.X = 100;
            Console.WriteLine("\n=> Changed p1.X\n");
            p1.Display();
            p2.Display();
        }

    }
}
