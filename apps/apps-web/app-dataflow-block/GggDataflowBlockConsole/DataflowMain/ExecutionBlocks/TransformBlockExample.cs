using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.ExecutionBlocks
{
    /// <summary>
    /// The TransformBlock<TInput,TOutput> class resembles the ActionBlock<TInput> class, 
    /// except that it acts as both a source and as a target. The delegate that you pass to a 
    /// TransformBlock<TInput,TOutput> object returns a value of type TOutput. The delegate that you 
    /// provide to a TransformBlock<TInput,TOutput> object can be of type System.Func<TInput, TOutput> 
    /// or type System.Func<TInput, Task>. When you use a TransformBlock<TInput,TOutput> object with 
    /// System.Func\<TInput, TOutput>, processing of each input element is considered completed when the 
    /// delegate returns. When you use a TransformBlock<TInput,TOutput> object used with System.Func<TInput, Task<TOutput>>, 
    /// processing of each input element is considered completed only when the returned Task object is completed. 
    /// As with ActionBlock<TInput>, by using these two mechanisms, you can use TransformBlock<TInput,TOutput> 
    /// for both synchronous and asynchronous processing of each input element.
    /// </summary>
    class TransformBlockExample
    {
        /// <summary>
        /// The following basic example creates a TransformBlock<TInput,TOutput> object 
        /// that computes the square root of its input. The TransformBlock<TInput,TOutput> 
        /// object takes Int32 values as input and produces Double values as output.
        /// </summary>
        public static void Run()
        {
            // Create a TransformBlock<int, double> object that 
            // computes the square root of its input.
            TransformBlock<int, double> transformBlock = new TransformBlock<int, double>(n => Math.Sqrt(n));
            // Post several messages to the block.
            transformBlock.Post(10);
            transformBlock.Post(20);
            transformBlock.Post(30);
            // Read the output messages from the block.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(transformBlock.Receive());
            }

        }
    }
}
