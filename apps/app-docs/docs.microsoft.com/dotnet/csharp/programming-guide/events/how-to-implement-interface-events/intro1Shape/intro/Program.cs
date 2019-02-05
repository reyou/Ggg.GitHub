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
            shape.ShapeChanged += (sender, eventArgs) =>
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Client received notification.");
                Console.WriteLine("Waiting for some time to process notification.");
                Thread.Sleep(TimeSpan.FromSeconds(3));
                Console.WriteLine("Notification processed.");
                Console.WriteLine("---------------------");
            };
            shape.ShapeChanged += (sender, eventArgs) =>
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Client received notification 2.");
                Console.WriteLine("Waiting for some time to process notification.");
                Thread.Sleep(TimeSpan.FromSeconds(3));
                Console.WriteLine("Notification processed 2.");
                Console.WriteLine("---------------------");
            };
            shape.ChangeShape();
            Console.ReadLine();
        }
    }
}
