using System;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataflowMain
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library#dataflow-block-completion
    /// </summary>
    public class DataflowBlockCompletion
    {
        public static void Run()
        {
            // Create an ActionBlock<int> object that prints its input
            // and throws ArgumentOutOfRangeException if the input
            // is less than zero.
            ActionBlock<int> throwIfNegative = new ActionBlock<int>(n =>
            {
                Console.WriteLine("n = {0}", n);
                if (n < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            });
            // Post values to the block.
            throwIfNegative.Post(0);
            throwIfNegative.Post(-1);
            throwIfNegative.Post(1);
            throwIfNegative.Post(-2);
            // Signals to the dataflow block that it shouldn't accept or produce
            // any more messages and shouldn't consume any more
            // postponed messages
            throwIfNegative.Complete();
            // Wait for completion in a try/catch block.
            try
            {
                // Completion: Gets a Task object that represents the asynchronous operation and completion
                // of the dataflow block
                throwIfNegative.Completion.Wait();
            }
            catch (AggregateException aggregateException)
            {
                // If an unhandled exception occurs during dataflow processing, all
                // exceptions are propagated through an AggregateException object.
                // Invokes a handler on each Exception contained by this AggregateException
                aggregateException.Handle(e =>
                {
                    Console.WriteLine("Encountered {0}: {1}", e.GetType().Name, e.Message);
                    return true;
                });
            }


        }
    }
}
