using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.BufferingBlocks
{
    /// <summary>
    /// The BufferBlock<T> class represents a general-purpose asynchronous messaging structure. 
    /// This class stores a first in, first out (FIFO) queue of messages that can be written 
    /// to by multiple sources or read from by multiple targets. When a target receives a 
    /// message from a BufferBlock<T> object, that message is removed from the message queue. 
    /// Therefore, although a BufferBlock<T> object can have multiple targets, only one target 
    /// will receive each message. The BufferBlock<T> class is useful when you want to pass 
    /// multiple messages to another component, and that component must receive each message.
    /// </summary>
    class BufferBlockExample
    {
        public static void Run()
        {
            // Create a BufferBlock<int> object.
            BufferBlock<int> bufferBlock = new BufferBlock<int>();
            // Post several messages to the block.
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }
            // Receive the messages back from the block.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }

        }

        public static void RunAsync()
        {
            BufferBlock<int> bufferBlock = new BufferBlock<int>();
            for (int i = 0; i < 10; i++)
            {
                bufferBlock.Post(i);
            }

            Task task1 = Task.Run(() =>
            {
                while (bufferBlock.TryReceive(out var item))
                {
                    Console.WriteLine("Worker 1 received: " + item + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                }
            });
            Task task2 = Task.Run(() =>
            {
                while (bufferBlock.TryReceive(out var item))
                {
                    Console.WriteLine("Worker 2 received: " + item + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                }
            });
            Task.WaitAll(task1, task2);
        }
    }
}
