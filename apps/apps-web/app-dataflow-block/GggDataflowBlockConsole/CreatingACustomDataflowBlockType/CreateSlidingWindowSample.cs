using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.CreatingACustomDataflowBlockType
{
    class CreateSlidingWindowSample
    {
        // Creates a IPropagatorBlock<T, T[]> object propagates data in a 
        // sliding window fashion.
        public static IPropagatorBlock<T, T[]> CreateSlidingWindow<T>(int windowSize)
        {
            // Create a queue to hold messages.
            Queue<T> queue = new Queue<T>();

            // The source part of the propagator holds arrays of size windowSize
            // and propagates data out to any connected targets.
            BufferBlock<T[]> source = new BufferBlock<T[]>();

            // The target part receives data and adds them to the queue.
            ActionBlock<T> target = new ActionBlock<T>(item =>
            {
                // Add the item to the queue.
                queue.Enqueue(item);
                // Remove the oldest item when the queue size exceeds the window size.
                if (queue.Count > windowSize)
                {
                    queue.Dequeue();
                }
                // Post the data in the queue to the source block when the queue size
                // equals the window size.
                if (queue.Count == windowSize)
                {
                    source.Post(queue.ToArray());
                }

            });

            // When the target is set to the completed state, propagate out any
            // remaining data and set the source to the completed state.
            target.Completion.ContinueWith(delegate
            {
                if (queue.Count > 0 && queue.Count < windowSize)
                {
                    source.Post(queue.ToArray());
                }
                source.Complete();
            });

            // Return a IPropagatorBlock<T, T[]> object that encapsulates the 
            // target and source blocks.
            // DataflowBlock.Encapsulate Encapsulates a target and a source into a single propagator
            return DataflowBlock.Encapsulate(target, source);

        }
    }
}
