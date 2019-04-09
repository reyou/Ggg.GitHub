using System;
using System.Threading;
using System.Threading.Tasks;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task task1 = Task.Run(() =>
            {
                Console.WriteLine("Calling Cancel...");
                cancellationTokenSource.Cancel();
                Task.Delay(5000).Wait();
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    Console.WriteLine("Warning! Cancellation Requested!");
                }
                Console.WriteLine("THIS WILL PRINT even if it is CANCELLED: Task ended delay...");
            });
            try
            {
                Console.WriteLine("About to wait for the task to complete...");
                task1.Wait(cancellationTokenSource.Token);
                Console.WriteLine("THIS WILL NOT PRINT!!!!");
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("{0}: The wait has been canceled. Task status: {1:G}",
                    e.GetType().Name, task1.Status);
                Thread.Sleep(6000);
                Console.WriteLine("After sleeping, the task status:  {0:G}", task1.Status);
            }
            cancellationTokenSource.Dispose();

            Console.WriteLine("End of Main Thread.");
            Console.ReadLine();
        }


    }
}
