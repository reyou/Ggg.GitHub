﻿using System;
using System.Threading;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****Synchronizing Threads *****\n");
            Printer p = new Printer();
            // Make 10 threads that are all pointing to the same
            // method on the same object.
            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"Worker thread #{i}"
                };
            }
            // Now start each one.
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            Console.ReadLine();
        }
    }

    internal class Printer
    {
        private readonly object _threadlock = new object();
        public void PrintNumbers()
        {
            lock (_threadlock)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Put thread to sleep for a random amount of time.
                    Random r = new Random();
                    Thread.Sleep(100 * r.Next(5));
                    Console.WriteLine("{0}: {1}, ", Thread.CurrentThread.Name, i);
                }
                Console.WriteLine();
            }

        }
    }
}
