using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.ReadWriteMessages
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-write-messages-to-and-read-messages-from-a-dataflow-block
    /// The following example uses the Post method to write to a BufferBlock<T> dataflow 
    /// block and the Receive method to read from the same object.
    /// </summary>
    class WritingToAndReadingSync
    {
        public static void Run()
        {
            // Create a BufferBlock<int> object.
            BufferBlock<int> bufferBlock = new BufferBlock<int>();

            // Post serveral message to block.
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

        /// <summary>
        /// You can also use the TryReceive method to read from a dataflow block, as 
        /// shown in the following example. The TryReceive method does not block the 
        /// current thread and is useful when you occasionally poll for data.
        /// </summary>
        public static void RunWithTryReceive()
        {
            BufferBlock<int> bufferBlock = new BufferBlock<int>();
            // Post more messages to the block.
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            // Receive the messages back from the block.
            int value;
            while (bufferBlock.TryReceive(out value))
            {
                Console.WriteLine(value);
            }


        }

        /// <summary>
        /// Because the Post method acts synchronously, the BufferBlock<T> object in the previous 
        /// examples receives all data before the second loop reads data. The following example extends 
        /// the first example by using Invoke to read from and write to the message block concurrently. 
        /// Because Invoke performs actions concurrently, the values are not written to the 
        /// BufferBlock<T> object in any specific order.
        /// </summary>
        public static void PostMethodActsSynchronously()
        {
            BufferBlock<int> bufferBlock = new BufferBlock<int>();
            Task post01 = Task.Run(() =>
            {
                bufferBlock.Post(0);
                bufferBlock.Post(1);
                bufferBlock.Post(2);
            });
            Task receive = Task.Run(() =>
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(bufferBlock.Receive());
                }
            });
            Task post2 = Task.Run(() =>
            {
                bufferBlock.Post(3);
            });
            Task.WaitAll(post01, receive, post2);
        }
    }
}
