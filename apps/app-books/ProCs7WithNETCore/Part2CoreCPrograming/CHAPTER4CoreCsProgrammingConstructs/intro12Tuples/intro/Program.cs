using System;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            (string FirstLetter, int TheNumber, string SecondLetter) valuesWithNames = ("a", 5, "c");
            (string FirstLetter, int TheNumber, string SecondLetter) valuesWithNames2 = (FirstLetter: "a", TheNumber: 5, SecondLetter: "c");
            Console.WriteLine($"First item: {valuesWithNames.FirstLetter}");
            Console.WriteLine($"Second item: {valuesWithNames.TheNumber}");
            Console.WriteLine($"Third item: {valuesWithNames.SecondLetter}");
            //Using the item notation still works!
            Console.WriteLine($"First item: {valuesWithNames.Item1}");
            Console.WriteLine($"Second item: {valuesWithNames.Item2}");
            Console.WriteLine($"Third item: {valuesWithNames.Item3}");
            Console.WriteLine($"*****************");
            Console.WriteLine($"First item 2: {valuesWithNames2.FirstLetter}");
            Console.WriteLine($"Second item 2: {valuesWithNames2.TheNumber}");
            Console.WriteLine($"Third item 2: {valuesWithNames2.SecondLetter}");
            Console.ReadLine();
        }
    }
}
