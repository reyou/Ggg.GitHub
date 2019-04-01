using System;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.BufferingBlocks
{
    /// <summary>
    /// The BroadcastBlock<T> class is useful when you must pass multiple messages to 
    /// another component, but that component needs only the most recent value. This 
    /// class is also useful when you want to broadcast a message to multiple components.
    /// </summary>
    class BroadcastBlockExample
    {
        /// <summary>
        /// The following basic example posts a Double value to a BroadcastBlock<T> 
        /// object and then reads that value back from that object several times. Because 
        /// values are not removed from BroadcastBlock<T> objects after they are read, 
        /// the same value is available every time.
        /// </summary>
        public static void Run()
        {
            // Create a BroadcastBlock<double> object.
            // Provides a buffer for storing at most one element at time, overwriting 
            // each message with the next as it arrives
            BroadcastBlock<double> broadcastBlock = new BroadcastBlock<double>(null);

            // Post a message to the block.
            broadcastBlock.Post(Math.PI);
            broadcastBlock.Post(Math.PI * 2);
            broadcastBlock.Post(1234);

            // Receive the messages back from the block several times.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(broadcastBlock.Receive());
            }

        }
    }
}
