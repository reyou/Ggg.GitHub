using System;
using System.Threading;
using System.Threading.Tasks;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Task.Run(() =>
            {
                Random rnd = new Random();
                long sum = 0;
                int n = 5000000;
                for (int ctr = 1; ctr <= n; ctr++)
                {
                    int number = rnd.Next(0, 101);
                    sum += number;
                    Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    if (ctr % 100 == 0)
                    {
                        Console.WriteLine("Task still runs Sum: {0}", sum);
                    }
                }
                Console.WriteLine("Total:   {0:N0}", sum);
                Console.WriteLine("Mean:    {0:N2}", sum / n);
                Console.WriteLine("N:       {0:N0}", n);
            });
            TimeSpan ts = TimeSpan.FromSeconds(3);
            // Wait(TimeSpan) is a synchronization method that causes the calling
            // thread (Main) to wait for the current task instance to complete
            // until one of the following occurs.
            if (!t.Wait(ts))
            {
                Console.WriteLine("The timeout interval elapsed.");
            }
            else
            {
                Console.WriteLine("The timeout interval NOT elapsed.");
            }

            Console.WriteLine("End of Main Thread.");
            Console.ReadLine();
        }
    }
}
