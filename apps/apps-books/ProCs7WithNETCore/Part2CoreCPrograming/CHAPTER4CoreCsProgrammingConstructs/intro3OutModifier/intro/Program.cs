using System;

namespace intro
{
    class Program
    {

        // Returning multiple output parameters.
        static void FillTheseValues(out int a, out string b, out bool c)
        {
            a = 9;
            b = "Enjoy your string.";
            c = true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Methods *****");
            FillTheseValues(out int i, out string str, out bool b);
            Console.WriteLine("Int is: {0}", i);
            Console.WriteLine("String is: {0}", str);
            Console.WriteLine("Boolean is: {0}", b);
            Console.ReadLine();
        }
    }
}
