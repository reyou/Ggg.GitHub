using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.ProducerConsumerPattern
{
    /// <summary>
    /// Demonstrates a basic producer and consumer pattern that uses dataflow
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-implement-a-producer-consumer-dataflow-pattern
    /// </summary>
    class DataflowProducerConsumer
    {
        // Demonstrates the production end of the producer and consumer pattern.
        // ITargetBlock Represents a dataflow block that is a target for data
        static void Produce(ITargetBlock<byte[]> target)
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

        // Demonstrates the consumption end of the producer and consumer pattern.
        static async Task<int> ConsumerAsync(ISourceBlock<byte[]> source)
        {
            // Initialize a counter to track the numaber of bytes that are processed.
            int bytesProcessed = 0;

            // Read from the source buffer until the source buffer has no
            // available output data.
            while (await source.OutputAvailableAsync())
            {
                byte[] data = source.Receive();

                // Increment the count of the bytes received.
                bytesProcessed += data.Length;
            }

            return bytesProcessed;
        }

        public static void Run()
        {
            // Create a BufferBlock<byte[]> object. This object servers as the
            // target block for the producer and the source block for the consumer.
            BufferBlock<byte[]> buffer = new BufferBlock<byte[]>();

            // Start the consumer. The Consume method runs asynchronously.
            Task<int> consumerAsync = ConsumerAsync(buffer);

            // Post source data to the dataflow block.
            Produce(buffer);

            // Wait for the consumer to process all data.
            consumerAsync.Wait();

            // Print the count of the bytes processed to the console.
            Console.WriteLine("Processed {0} bytes.", consumerAsync.Result);
        }

    }
}
