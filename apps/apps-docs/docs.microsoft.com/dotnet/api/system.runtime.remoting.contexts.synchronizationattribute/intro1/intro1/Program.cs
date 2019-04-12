using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace intro1
{
    class Program
    {

        // Context-bound type with the Synchronization context attribute.
        [Synchronization()]
        public class SampleSynchronized : ContextBoundObject
        {
            // A method that does some work, and returns the square of the given number.
            public int Square(int i)
            {

                // Console.Write("The hash of the thread executing ");
                // Console.WriteLine("SampleSynchronized.Square is: {0}", Thread.CurrentThread.GetHashCode());
                var result = i * i;
                for (int j = 0; j < result; j++)
                {
                    Console.WriteLine(string.Format("Printing results for {0}: {1}", i, result));
                }
                Console.WriteLine("");
                return result;
            }
        }
        static void Main(string[] args)
        {
            SampleSynchronized sampleSynchronized = new SampleSynchronized();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                Thread thread = new Thread(o =>
                {
                    sampleSynchronized.Square(i1);

                });
                threads.Add(thread);
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            Console.WriteLine("Main app is finished.");
            Console.ReadKey();

        }
    }
}
