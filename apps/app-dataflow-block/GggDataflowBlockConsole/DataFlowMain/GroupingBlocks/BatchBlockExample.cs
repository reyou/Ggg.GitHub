using System;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.GroupingBlocks
{
    /// <summary>
    /// The BatchBlock<T> class combines sets of input data, which are known as batches, 
    /// into arrays of output data. You specify the size of each batch when you create a BatchBlock<T> 
    /// object. When the BatchBlock<T> object receives the specified count of input elements, it 
    /// asynchronously propagates out an array that contains those elements. If a BatchBlock<T> object 
    /// is set to the completed state but does not contain enough elements to form a batch, it 
    /// propagates out a final array that contains the remaining input elements.
    /// </summary>
    class BatchBlockExample
    {
        /// <summary>
        /// The following basic example posts several Int32 values to a BatchBlock<T> object 
        /// that holds ten elements in a batch. To guarantee that all values propagate out of the 
        /// BatchBlock<T>, this example calls the Complete method. The Complete method sets the 
        /// BatchBlock<T> object to the completed state, and therefore, the BatchBlock<T> object 
        /// propagates out any remaining elements as a final batch.
        /// </summary>
        public static void Run()
        {
            // Create a BatchBlock<int> object that holds ten
            // elements per batch.
            // Provides a dataflow block that batches inputs into arrays
            // Below every batch will have 10 items.
            BatchBlock<int> batchBlock = new BatchBlock<int>(10);
            Console.WriteLine("BatchBlock<int> batchBlock = new BatchBlock<int>(10);");
            // Post several values to the block.
            for (int i = 0; i < 13; i++)
            {
                batchBlock.Post(i);
                Console.WriteLine("Posted to batchBlock {0}.", i);
            }

            // Set the block to the completed state. This causes
            // the block to propagate out any any remaining
            // values as a final batch.
            batchBlock.Complete();
            Console.WriteLine("batchBlock.Complete();");
            // Print the sum of both batches.
            int length1 = batchBlock.Receive().Length;
            int length2 = batchBlock.Receive().Length;
            Console.WriteLine("The total element count in batch-1 is {0}.", length1);
            Console.WriteLine("The total element count in batch-2 is {0}.", length2);
        }
    }
}
