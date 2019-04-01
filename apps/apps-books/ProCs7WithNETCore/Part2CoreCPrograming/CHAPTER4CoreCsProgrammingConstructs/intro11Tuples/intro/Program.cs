using System;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            (string, int, string) values = ("a", 5, "c");
            (string, int, string) values2 = ("a", 5, "c");
            Console.WriteLine(values);
            Console.WriteLine(values2);
            Console.WriteLine($"First item: {values.Item1}");
            Console.WriteLine($"Second item: {values.Item2}");
            Console.WriteLine($"Third item: {values.Item3}");
            Console.ReadLine();
        }
    }
}
