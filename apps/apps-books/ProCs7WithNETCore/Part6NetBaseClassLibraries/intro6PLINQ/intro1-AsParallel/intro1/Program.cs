using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing");
            Console.WriteLine();
            Task.Factory.StartNew(ProcessIntData);
            Task.Factory.StartNew(ProcessIntDataAsParallel);
            Console.ReadLine();
        }
        static void ProcessIntData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 10_000_000).ToArray();
            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order.
            int[] modThreeIsZero = (from num in source
                                    where num % 3 == 0
                                    orderby num descending
                                    select num).ToArray();
            sw.Stop();
            Console.WriteLine($"Found { modThreeIsZero.Count()} numbers that match query!");
            Console.WriteLine($"ProcessIntData took {sw.ElapsedMilliseconds} Milliseconds.");
            Console.WriteLine();
        }
        static void ProcessIntDataAsParallel()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 10_000_000).ToArray();
            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order.
            int[] modThreeIsZero = (from num in source.AsParallel()
                                    where num % 3 == 0
                                    orderby num descending
                                    select num).ToArray();
            sw.Stop();
            Console.WriteLine($"Found { modThreeIsZero.Count()} numbers that match query!");
            Console.WriteLine($"ProcessIntDataAsParallel took {sw.ElapsedMilliseconds} Milliseconds.");
            Console.WriteLine();
        }
    }

}
