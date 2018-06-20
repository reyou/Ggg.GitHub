using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.GroupingBlocks
{
    class BatchedJoinBlockExample
    {
        public static void Run()
        {
            // For demonstration, create a Func<int, int> that 
            // returns its argument, or throws ArgumentOutOfRangeException
            // if the argument is less than zero.
            Func<int, int> DoWork = n =>
            {
                if (n < 0)
                {
                    Console.WriteLine("ArgumentOutOfRangeException: " + n);
                    throw new ArgumentOutOfRangeException();
                }
                return n;
            };

            // Create a BatchedJoinBlock<int, Exception> object that holds 
            // seven elements per batch.
            BatchedJoinBlock<int, Exception> batchedJoinBlock = new BatchedJoinBlock<int, Exception>(7);
            // Post several items to the block.
            foreach (int i in new int[] { 5, 6, -7, -22, 13, 55, 0 })
            {
                try
                {
                    // Post the result of the worker to the 
                    // first target of the block.
                    batchedJoinBlock.Target1.Post(DoWork(i));
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // If an error occurred, post the Exception to the 
                    // second target of the block.
                    batchedJoinBlock.Target2.Post(e);
                }
            }

            // Read the results from the block.
            Tuple<IList<int>, IList<Exception>> results = batchedJoinBlock.Receive();

            // Print the results to the console.
            // Print the results.
            foreach (int n in results.Item1)
            {
                Console.WriteLine(n);
            }

            // Print failures.
            foreach (Exception e in results.Item2)
            {
                Console.WriteLine(e.Message);
            }



        }
    }
}
