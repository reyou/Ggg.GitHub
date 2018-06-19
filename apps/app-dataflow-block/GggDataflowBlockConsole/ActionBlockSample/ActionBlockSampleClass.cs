using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.ActionBlockSample
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.dataflow.actionblock-1?view=netcore-2.1
    /// </summary>
    class ActionBlockSampleClass
    {
        // Performs several computations by using dataflow and returns the elapsed
        // time required to perform the computations.
        static TimeSpan TimeDataflowComputations(int maxDegreeOfParallelism, int messageCount)
        {
            // Create an ActionBlock<int> that performs some work.
            ActionBlock<int> workerBlock = new ActionBlock<int>(
                // Simulate work by suspending the current thread.
                millisecondsTimeout =>
                {
                    Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("ActionBlock is performing some task...");
                    Thread.Sleep(millisecondsTimeout);
                },
                // Specify a maximum degree of parallelism.
                /*Provides options used to configure the processing performed by dataflow blocks
                 that process each message through the invocation of a user-provided delegate. 
                 These are dataflow blocks such as ActionBlock<TInput> and TransformBlock<TInput,TOutput>*/
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParallelism
                });
            // Compute the time that it takes for several messages to 
            // flow through the dataflow block.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < messageCount; i++)
            {
                workerBlock.Post(1000);
            }
            workerBlock.Complete();
            // Wait for all messages to propagate through the network.
            workerBlock.Completion.Wait();
            // Stop the timer and return the elapsed number of milliseconds.
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public static void Run()
        {
            int maxDegreeOfParallelism = 8;
            int messageCount = 10;
            TimeSpan timeDataflowComputations = TimeDataflowComputations(maxDegreeOfParallelism, messageCount);
            Console.WriteLine("timeDataflowComputations: " + timeDataflowComputations);
        }
    }
}
