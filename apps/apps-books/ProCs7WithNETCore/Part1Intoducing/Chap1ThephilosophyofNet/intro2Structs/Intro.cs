using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro1
{

    // A C# structure type.
    struct Point
    {
        // Structures can contain fields.
        public int XPos, YPos;
        // Structures can contain parameterized constructors.
        public Point(int x, int y)
        {
            XPos = x;
            YPos = y;
        }
        // Structures may define methods.
        public void PrintPosition()
        {
            Console.WriteLine("({0}, {1})", XPos, YPos);
        }
    }
    // This class contains the app's entry point.
    class Program
    {
        static void Main()
        {
            Point point = new Point();
            point.PrintPosition();
            point = new Point(10, 20);
            point.PrintPosition();
            Console.ReadLine();
        }
    }

}
