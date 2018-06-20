using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.UnlinkDataflowBlocks
{
    /// <summary>
    /// The following example creates three TransformBlock<TInput,TOutput> objects,
    /// each of which calls the TrySolution method to compute a value. This example requires
    /// only the result from the first call to TrySolution to finish.
    /// </summary>
    class DataflowReceiveAny
    {
        // Demonstrates how to unlink dataflow blocks.
        // Receives the value from the first provided source that has 
        // a message.
        /*ISourceBlock<TOutput> Interface Represents a dataflow block that is a source of data*/
        public static T ReceiveFromAny<T>(params ISourceBlock<T>[] sources)
        {
            // Create a WriteOnceBlock<T> object and link it to each source block.
            /*Provides a buffer for receiving and storing at most one element
             in a network of dataflow blocks*/
            WriteOnceBlock<T> writeOnceBlock = new WriteOnceBlock<T>(e => e);
            foreach (ISourceBlock<T> source in sources)
            {
                // Setting MaxMessages to one instructs
                // the source block to unlink from the WriteOnceBlock<T> object
                // after offering the WriteOnceBlock<T> object one message.
                source.LinkTo(writeOnceBlock, new DataflowLinkOptions
                {
                    MaxMessages = 1
                });
            }
            // Return the first value that is offered to the WriteOnceBlock object.
            T receive = writeOnceBlock.Receive();
            return receive;
        }

        // Demonstrates a function that takes several seconds to produce a result.
        static int TrySolution(int n, CancellationToken ct)
        {
            // Simulate a lengthy operation that completes within three seconds
            // or when the provided CancellationToken object is cancelled.
            SpinWait.SpinUntil(() =>
                {
                    bool ctIsCancellationRequested = ct.IsCancellationRequested;
                    if (ctIsCancellationRequested)
                    {
                        Console.WriteLine("CancellationRequested! n: " + n);
                    }
                    return ctIsCancellationRequested;
                },
                new Random().Next(3000));

            // Return a value.
            return n + n - n;
        }

        public static void Run()
        {
            // Create a shared CancellationTokenSource object to enable the 
            // TrySolution method to be cancelled.
            CancellationTokenSource cts = new CancellationTokenSource();
            // Create three TransformBlock<int, int> objects. 
            // Each TransformBlock<int, int> object calls the TrySolution method.
            Func<int, int> action = n => TrySolution(n, cts.Token);
            TransformBlock<int, int> trySolution1 = new TransformBlock<int, int>(action);
            TransformBlock<int, int> trySolution2 = new TransformBlock<int, int>(action);
            TransformBlock<int, int> trySolution3 = new TransformBlock<int, int>(action);

            // Post data to each TransformBlock<int, int> object.
            trySolution1.Post(11);
            trySolution2.Post(21);
            trySolution3.Post(31);

            // Call the ReceiveFromAny<T> method to receive the result from the 
            // first TransformBlock<int, int> object to finish.
            int result = ReceiveFromAny(trySolution1, trySolution2, trySolution3);
            // Cancel all calls to TrySolution that are still active.
            cts.Cancel();
            // Print the result to the console.
            Console.WriteLine("The solution is {0}.", result);
            cts.Dispose();
        }
    }
}
