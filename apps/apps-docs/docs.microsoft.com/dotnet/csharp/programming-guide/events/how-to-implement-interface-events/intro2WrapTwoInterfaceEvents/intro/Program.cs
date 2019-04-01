using System;
using System.Threading;

namespace intro
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-implement-interface-events#to-implement-interface-events-in-a-class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Shape shape = new Shape();
            Subscriber1 sub = new Subscriber1(shape);
            Subscriber2 sub2 = new Subscriber2(shape);
            shape.Draw();

            // Keep the console window open in debug mode.
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }

}
