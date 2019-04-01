using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.CreatingACustomDataflowBlockType
{
    // Propagates data in a sliding window fashion.
    public class SlidingWindowBlockSample<T> : IPropagatorBlock<T, T[]>,
        IReceivableSourceBlock<T[]>
    {
        // The size of the window.
        private readonly int _mWindowSize;
        // The target part of the block.
        private readonly ITargetBlock<T> _mTarget;
        // The source part of the block.
        private readonly IReceivableSourceBlock<T[]> _mSource;
        // Constructs a SlidingWindowBlock object.
        public SlidingWindowBlockSample(int windowSize)
        {
            // Create a queue to hold messages.
            var queue = new Queue<T>();

            // The source part of the propagator holds arrays of size windowSize
            // and propagates data out to any connected targets.
            var source = new BufferBlock<T[]>();

            // The target part receives data and adds them to the queue.
            var target = new ActionBlock<T>(item =>
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
                    source.Post(queue.ToArray());
                source.Complete();
            });

            _mWindowSize = windowSize;
            _mTarget = target;
            _mSource = source;

        }

        // Retrieves the size of the window.
        public int WindowSize { get { return _mWindowSize; } }
        #region IReceivableSourceBlock<TOutput> members

        // Attempts to synchronously receive an item from the source.
        public bool TryReceive(Predicate<T[]> filter, out T[] item)
        {
            return _mSource.TryReceive(filter, out item);
        }

        // Attempts to remove all available elements from the source into a new 
        // array that is returned.
        public bool TryReceiveAll(out IList<T[]> items)
        {
            return _mSource.TryReceiveAll(out items);
        }

        #endregion

        #region ISourceBlock<TOutput> members

        // Links this dataflow block to the provided target.
        public IDisposable LinkTo(ITargetBlock<T[]> target, DataflowLinkOptions linkOptions)
        {
            return _mSource.LinkTo(target, linkOptions);
        }

        // Called by a target to reserve a message previously offered by a source 
        // but not yet consumed by this target.
        bool ISourceBlock<T[]>.ReserveMessage(DataflowMessageHeader messageHeader,
           ITargetBlock<T[]> target)
        {
            return _mSource.ReserveMessage(messageHeader, target);
        }

        // Called by a target to consume a previously offered message from a source.
        T[] ISourceBlock<T[]>.ConsumeMessage(DataflowMessageHeader messageHeader,
           ITargetBlock<T[]> target, out bool messageConsumed)
        {
            return _mSource.ConsumeMessage(messageHeader,
               target, out messageConsumed);
        }

        // Called by a target to release a previously reserved message from a source.
        void ISourceBlock<T[]>.ReleaseReservation(DataflowMessageHeader messageHeader,
           ITargetBlock<T[]> target)
        {
            _mSource.ReleaseReservation(messageHeader, target);
        }

        #endregion

        #region ITargetBlock<TInput> members

        // Asynchronously passes a message to the target block, giving the target the 
        // opportunity to consume the message.
        DataflowMessageStatus ITargetBlock<T>.OfferMessage(DataflowMessageHeader messageHeader,
           T messageValue, ISourceBlock<T> source, bool consumeToAccept)
        {
            return _mTarget.OfferMessage(messageHeader,
               messageValue, source, consumeToAccept);
        }

        #endregion

        #region IDataflowBlock members

        // Gets a Task that represents the completion of this dataflow block.
        public Task Completion { get { return _mSource.Completion; } }

        // Signals to this target block that it should not accept any more messages, 
        // nor consume postponed messages. 
        public void Complete()
        {
            _mTarget.Complete();
        }

        public void Fault(Exception error)
        {
            _mTarget.Fault(error);
        }

        #endregion
    }
}
