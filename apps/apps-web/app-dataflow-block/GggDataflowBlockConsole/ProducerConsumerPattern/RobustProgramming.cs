using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.ProducerConsumerPattern
{
    /// <summary>
    /// This example uses just one consumer to process the source data. 
    /// If you have multiple consumers in your application, use the TryReceive 
    /// method to read data from the source block, as shown in the 
    /// following example.
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-implement-a-producer-consumer-dataflow-pattern
    /// </summary>
    class RobustProgramming
    {
        // Demonstrates the consumption end of the producer and consumer pattern.
        // IReceivableSourceBlock: Represents a dataflow block that supports receiving messages without linking
        static async Task<int> ConsumeAsync(IReceivableSourceBlock<byte[]> source)
        {
            // Initialize a counter to track the number of bytes that are processed.
            int bytesProcessed = 0;

            // Read from the source buffer until the source buffer has no 
            // available output data.
            while (await source.OutputAvailableAsync())
            {
                byte[] data;
                while (source.TryReceive(out data))
                {
                    // Increment the count of bytes received.
                    bytesProcessed += data.Length;
                }
            }

            return bytesProcessed;
        }

        static async Task<int> ConsumeAsync2(IReceivableSourceBlock<byte[]> source)
        {
            // Initialize a counter to track the number of bytes that are processed.
            int bytesProcessed = 0;

            // Read from the source buffer until the source buffer has no 
            // available output data.
            while (await source.OutputAvailableAsync())
            {
                byte[] data;
                while (source.TryReceive(out data))
                {
                    // Increment the count of bytes received.
                    bytesProcessed += data.Length;
                }
            }

            return bytesProcessed;
        }

        // ITargetBlock: Represents a dataflow block that is a target for data
        static void ProduceAsync(ITargetBlock<byte[]> target)
        {
            // Create a Random object to generate random data.
            Random rand = new Random();

            // In a loop, fill a buffer with random data and
            // post the buffer to target block.
            for (int i = 0; i < 100; i++)
            {
                // Create an array to hold random byte data.
                byte[] buffer = new byte[1024];

                // Fill the buffer with random bytes.
                rand.NextBytes(buffer);

                // Post the result to the message block.
                target.Post(buffer);
            }

            // Set the target to the completed state to signal to the consumer
            // that no more data will be available.
            // Signals to the IDataflowBlock that it should not accept nor 
            // produce any more messages nor consume any more postponed messages
            target.Complete();
        }

        public static void Run()
        {
            IReceivableSourceBlock<byte[]> target = new BufferBlock<byte[]>();
            Task<int> consumeAsync = ConsumeAsync(target);
            Task<int> consumeAsync2 = ConsumeAsync2(target);
            ProduceAsync((ITargetBlock<byte[]>)target);
            Task.WaitAll(consumeAsync, consumeAsync2);
            int consumeAsyncResult = consumeAsync.Result;
            int consumeAsyncResult2 = consumeAsync2.Result;
            Console.WriteLine("Processed {0} bytes. (consumeAsyncResult)", consumeAsyncResult);
            Console.WriteLine("Processed {0} bytes. (consumeAsyncResult2)", consumeAsyncResult2);
        }

    }
}
