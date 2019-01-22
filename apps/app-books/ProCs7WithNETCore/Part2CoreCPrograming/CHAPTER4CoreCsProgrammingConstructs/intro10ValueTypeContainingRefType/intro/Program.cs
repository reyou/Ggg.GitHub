using System;
using System.Drawing;
using Newtonsoft.Json;

namespace intro
{ // Classes are always reference types.
    class Program
    {
        static void Main(string[] args)
        {
            ValueTypeContainingRefType();
            Console.ReadLine();
        }
        // Assigning two intrinsic value types results in
        // two independent variables on the stack.
        static void ValueTypeContainingRefType()
        {
            // Create the first Rectangle.
            Console.WriteLine("-> Creating r1");
            Rectangle2 r1 = new Rectangle2("First Rect", 10, 10, 50, 50);

            // Now assign a new Rectangle to r1.
            Console.WriteLine("-> Assigning r2 to r1");
            Rectangle2 r2 = r1;

            // Change some values of r2.
            Console.WriteLine("-> Changing values of r2");
            r2.RectInfo.InfoString = "This is new info!";
            r2.RectBottom = 4444;
            // Print values of both rectangles.
            r1.Display();
            r2.Display();
        }

    }

    internal class Rectangle2
    {
        private Rectangle _rectangle;
        private string _title;

        public Rectangle2(string firstRect, int x, int y, int width, int height)
        {
            this._title = firstRect;
            this._rectangle = new Rectangle(x, y, width, height);
            RectInfo = new RectInfo();
        }

        public RectInfo RectInfo { get; set; }
        public int RectBottom { get; set; }

        public void Display()
        {
            Console.WriteLine("String = {0}", RectInfo.InfoString);
        }
    }

    internal class RectInfo
    {
        public string InfoString { get; set; }
    }
}
