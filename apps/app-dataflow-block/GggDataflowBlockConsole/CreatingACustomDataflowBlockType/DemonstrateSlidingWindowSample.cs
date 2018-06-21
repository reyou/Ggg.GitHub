using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.CreatingACustomDataflowBlockType
{
    class DemonstrateSlidingWindowSample
    {
        // Demonstrates usage of the sliding window block by sending the provided
        // values to the provided propagator block and printing the output of 
        // that block to the console.
        static void DemonstrateSlidingWindow<T>(IPropagatorBlock<T, T[]> slidingWindow,
            IEnumerable<T> values)
        {
            // Create an action block that prints arrays of data to the console.
            string windowComma = string.Empty;
            var printWindow = new ActionBlock<T[]>(window =>
            {
                Console.Write(windowComma);
                Console.Write("{");

                string comma = string.Empty;
                foreach (T item in window)
                {
                    Console.Write(comma);
                    Console.Write(item);
                    comma = ",";
                }
                Console.Write("}");

                windowComma = ", ";
            });

            // Link the printer block to the sliding window block.
            slidingWindow.LinkTo(printWindow);

            // Set the printer block to the completed state when the sliding window
            // block completes.
            slidingWindow.Completion.ContinueWith(delegate { printWindow.Complete(); });

            // Print an additional newline to the console when the printer block completes.
            var completion = printWindow.Completion.ContinueWith(delegate { Console.WriteLine(); });

            // Post the provided values to the sliding window block and then wait
            // for the sliding window block to complete.
            foreach (T value in values)
            {
                slidingWindow.Post(value);
            }
            slidingWindow.Complete();

            // Wait for the printer to complete and perform its final action.
            completion.Wait();
        }

        public static void Run()
        {

            Console.Write("Using the DataflowBlockExtensions.Encapsulate method ");
            Console.WriteLine("(T=int, windowSize=3):");
            DemonstrateSlidingWindow(CreateSlidingWindowSample.CreateSlidingWindow<int>(3), Enumerable.Range(0, 10));

            Console.WriteLine();

            var slidingWindow = new SlidingWindowBlockSample<char>(4);

            Console.Write("Using SlidingWindowBlock<T> ");
            Console.WriteLine("(T=char, windowSize={0}):", slidingWindow.WindowSize);
            DemonstrateSlidingWindow(slidingWindow, from n in Enumerable.Range(65, 10)
                                                    select (char)n);
        }
    }
}
