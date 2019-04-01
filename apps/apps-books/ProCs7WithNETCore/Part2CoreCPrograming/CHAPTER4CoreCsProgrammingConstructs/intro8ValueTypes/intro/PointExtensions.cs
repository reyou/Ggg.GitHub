using System;
using System.Drawing;

namespace intro
{
    public static class PointExtensions
    {
        public static void Display(this Point point)
        {
            Console.WriteLine(point.ToString());
        }
    }
}