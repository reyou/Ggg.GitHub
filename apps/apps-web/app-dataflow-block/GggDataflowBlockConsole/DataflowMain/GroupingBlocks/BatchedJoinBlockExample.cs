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
                    Console.WriteLine("DoWork.ArgumentOutOfRangeException: " + n);
                    throw new ArgumentOutOfRangeException();
                }
                return n;
            };

            // Create a BatchedJoinBlock<int, Exception> object that holds 
            // seven elements per batch.
            // BatchedJoinBlock<T1,T2> Class
            // Provides a dataflow block that batches a specified number of inputs of
            // potentially differing types provided to one or more of its targets
            BatchedJoinBlock<int, Exception> batchedJoinBlock = new BatchedJoinBlock<int, Exception>(7);
            Console.WriteLine("BatchedJoinBlock<int, Exception> batchedJoinBlock = new BatchedJoinBlock<int, Exception>(7);");
            // Post several items to the block.
            int[] ints = { 5, 6, -7, -22, 13, 55, 0 };
            Console.WriteLine("int[] ints = {5, 6, -7, -22, 13, 55, 0};");
            foreach (int i in ints)
            {
                try
                {
                    // Post the result of the worker to the 
                    // first target of the block.
                    Console.WriteLine("try: batchedJoinBlock.Target1.Post(DoWork({0}));", i);
                    batchedJoinBlock.Target1.Post(DoWork(i));

                }
                catch (ArgumentOutOfRangeException e)
                {
                    // If an error occurred, post the Exception to the 
                    // second target of the block.
                    Console.WriteLine("catch: batchedJoinBlock.Target2.Post({0});", e.Message);
                    batchedJoinBlock.Target2.Post(e);
                }
            }

            // Read the results from the block.
            Console.WriteLine("Tuple<IList<int>, IList<Exception>> results = batchedJoinBlock.Receive();");
            Tuple<IList<int>, IList<Exception>> results = batchedJoinBlock.Receive();

            // Print the results to the console.
            // Print the results.
            IList<int> resultsItem1 = results.Item1;
            foreach (int n in resultsItem1)
            {
                Console.WriteLine("results.Item1: " + n);
            }

            // Print failures.
            IList<Exception> resultsItem2 = results.Item2;
            foreach (Exception e in resultsItem2)
            {
                Console.WriteLine("results.Item2: " + e.Message);
            }



        }
    }
}
