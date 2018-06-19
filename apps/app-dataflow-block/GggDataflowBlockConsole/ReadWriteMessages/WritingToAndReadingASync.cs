using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole
{
    /// <summary>
    /// The following example uses the SendAsync method to asynchronously write to a BufferBlock<T> 
    /// object and the ReceiveAsync method to asynchronously read from the same object. 
    /// This example uses the async and await operators (Async and Await in Visual Basic) 
    /// to asynchronously send data to and read data from the target block. The SendAsync 
    /// method is useful when you must enable a dataflow block to postpone messages. 
    /// The ReceiveAsync method is useful when you want to act on data when that data 
    /// becomes available. For more information about how messages propagate among message 
    /// blocks, see the section Message Passing in Dataflow.
    /// </summary>
    class WritingToAndReadingASync
    {
        public static async Task Run()
        {
            BufferBlock<int> bufferBlock = new BufferBlock<int>();
            // Post more messages to the block asynchronously.
            for (int i = 0; i < 10; i++)
            {
                await bufferBlock.SendAsync(i);
            }

            // Asynchronously receive the messages back from the block.
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(await bufferBlock.ReceiveAsync());
            }

        }
    }
}
