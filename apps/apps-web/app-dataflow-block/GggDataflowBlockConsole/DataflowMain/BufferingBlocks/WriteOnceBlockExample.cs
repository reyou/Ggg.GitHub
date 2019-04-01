using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain
{
    /// <summary>
    /// The WriteOnceBlock<T> class resembles the BroadcastBlock<T> class, except that a 
    /// WriteOnceBlock<T> object can be written to one time only. You can think of 
    /// WriteOnceBlock<T> as being similar to the C# readonly (ReadOnly in Visual Basic) keyword, 
    /// except that a WriteOnceBlock<T> object becomes immutable after it receives a value instead 
    /// of at construction. Like the BroadcastBlock<T> class, when a target receives a message 
    /// from a WriteOnceBlock<T> object, that message is not removed from that object. 
    /// Therefore, multiple targets receive a copy of the message. The WriteOnceBlock<T> 
    /// class is useful when you want to propagate only the first of multiple messages.
    /// </summary>
    class WriteOnceBlockExample
    {
        public static void Run()
        {
            // Create a WriteOnceBlock<string> object.
            // Provides a buffer for receiving and storing at most one element in a network of dataflow blocks
            WriteOnceBlock<string> writeOnceBlock = new WriteOnceBlock<string>(null);

            // Post several messages to the block in parallel. 
            // The first message to be received is written to the block. 
            // Subsequent messages are discarded.
            // Executes each of the provided actions, possibly in parallel
            Parallel.Invoke(
                () => writeOnceBlock.Post("Message 1. ThreadId: " + Thread.CurrentThread.ManagedThreadId),
                () => writeOnceBlock.Post("Message 2. ThreadId: " + Thread.CurrentThread.ManagedThreadId),
                () => writeOnceBlock.Post("Message 3. ThreadId: " + Thread.CurrentThread.ManagedThreadId));

            // Receive the message from the block.
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(writeOnceBlock.Receive());
            }

        }
    }
}
