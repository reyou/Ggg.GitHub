using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole
{
    /// <summary>
    /// The following example shows the complete code for this document.
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-write-messages-to-and-read-messages-from-a-dataflow-block#a-complete-example
    /// </summary>
    class CompleteExample
    {

        // Demonstrates asynchronous dataflow operations.
        static async Task AsyncSendReceive(BufferBlock<int> bufferBlock)
        {
            Console.WriteLine("Post more messages to the block asynchronously");
            for (int i = 0; i < 3; i++)
            {
                await bufferBlock.SendAsync(i);
            }

            Console.WriteLine("Asynchronously receive the messages back from the block");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(await bufferBlock.ReceiveAsync());
            }
        }

        public static void Run()
        {
            Console.WriteLine("Create a BufferBlock<int> object");
            BufferBlock<int> bufferBlock = new BufferBlock<int>();

            Console.WriteLine("Post several messages to the block");
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            Console.WriteLine("Receive the messages back from the block");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }

            Console.WriteLine("Post more messages to the block");
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }

            Console.WriteLine("Receive the messages back from the block");
            int value;
            while (bufferBlock.TryReceive(out value))
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("Write to and read from the message block concurrently");
            Task post01 = Task.Run(() =>
            {
                bufferBlock.Post(0);
                bufferBlock.Post(1);
            });
            Task receive = Task.Run(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(bufferBlock.Receive());
                }
            });
            Task post2 = Task.Run(() =>
            {
                bufferBlock.Post(2);
            });
            Task.WaitAll(post01, receive, post2);

            Console.WriteLine("Demonstrate asynchronous dataflow operations");
            AsyncSendReceive(bufferBlock).Wait();

        }
    }
}
