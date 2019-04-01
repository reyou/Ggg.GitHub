using System;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Delegates as event enablers *****\n");
            // First, make a Car object.
            Car c1 = new Car("SlugBug", 100, 10);
            // Register multiple targets for the notifications.
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent2));
            // Speed up (this will trigger the events).
            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);
            Console.ReadLine();
        }
        // We now have TWO methods that will be called by the Car
        // when sending notifications.
        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("***********************************\n");
        }
        public static void OnCarEngineEvent2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }
    }

}
