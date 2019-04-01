using System;
using Newtonsoft.Json;

namespace intro
{
    public class PointRef
    {
        // Same members as the Point structure...
        // Be sure to change your constructor name to PointRef!
        public PointRef(int XPos, int YPos)
        {
            X = XPos;
            Y = YPos;
        }

        public int Y { get; set; }

        public int X { get; set; }

        public void Display()
        {
            Console.WriteLine("X: {0} Y: {1}", X, Y);
        }
    }
}