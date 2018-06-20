using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.ExecutionBlocks
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library#actionblockt
    /// The ActionBlock<TInput> class is a target block that calls a delegate when it receives data. 
    /// Think of a ActionBlock<TInput> object as a delegate that runs asynchronously when data becomes available. 
    /// The delegate that you provide to an ActionBlock<TInput> object can be of type Action or type 
    /// System.Func\<TInput, Task>. When you use an ActionBlock<TInput> object with Action, 
    /// processing of each input element is considered completed when the delegate returns. 
    /// When you use an ActionBlock<TInput> object with System.Func\<TInput, Task>, processing 
    /// of each input element is considered completed only when the returned Task object is completed. 
    /// By using these two mechanisms, you can use ActionBlock<TInput> for both synchronous 
    /// and asynchronous processing of each input element.
    /// </summary>
    class ActionBlockExample
    {
        /// <summary>
        /// The following basic example posts multiple Int32 values to an ActionBlock<TInput> object. 
        /// The ActionBlock<TInput> object prints those values to the console. This example then sets 
        /// the block to the completed state and waits for all dataflow tasks to finish.
        /// </summary>
        public static void Run()
        {
            // Create an ActionBlock<int> object that prints values
            // to the console.
            ActionBlock<int> actionBlock = new ActionBlock<int>(n =>
            {
                Console.WriteLine("ActionBlockReceived: " + n);
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("");
            });
            // Post several messages to the block.
            for (int i = 0; i < 3; i++)
            {
                actionBlock.Post(i * 10);
            }
            // Set the block to the completed state and wait for all 
            // tasks to finish.
            // Signals to the dataflow block that it shouldn't accept or produce any more 
            // messages and shouldn't consume any more postponed messages
            actionBlock.Post(1234);
            actionBlock.Complete();
            // this will not be posted
            // also will not throw any exception.
            actionBlock.Post(9999);
            actionBlock.Completion.Wait();
        }
    }
}
