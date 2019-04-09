using System;
using System.Threading;
using System.Threading.Tasks;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            // Signals to a CancellationToken that it should be canceled
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(CancelToken);
            thread.Start(cancellationTokenSource);

            Task task = Task.Run(() =>
            {
                Task.Delay(5000).Wait();
                Console.WriteLine("Task ended delay...");
            });
            try
            {
                MainThreadMethod(task, cancellationTokenSource);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("{0}: The wait has been canceled. Task status: {1:G}",
                    e.GetType().Name, task.Status);
                Thread.Sleep(4000);
                Console.WriteLine("After sleeping, the task status:  {0:G}", task.Status);
                cancellationTokenSource.Dispose();
            }

            Console.WriteLine("End of Main Thread.");
            Console.ReadLine();
        }

        private static void MainThreadMethod(Task task, CancellationTokenSource cancellationTokenSource)
        {
            Console.WriteLine("About to wait completion of task {0}", task.Id);
            bool result = task.Wait(1510, cancellationTokenSource.Token);
            Console.WriteLine("THIS WILL NOT PRINT: Wait completed normally: {0}", result);
            Console.WriteLine("THIS WILL NOT PRINT: The task status:  {0:G}", task.Status);
        }

        private static void CancelToken(Object obj)
        {
            Thread.Sleep(1500);
            Console.WriteLine("Canceling the cancellation token from thread {0}...", Thread.CurrentThread.ManagedThreadId);
            CancellationTokenSource source = obj as CancellationTokenSource;
            if (source != null)
            {
                source.Cancel();
            }
        }
    }
}
