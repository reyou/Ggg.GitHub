using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.CreatingADataflowPipeline
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/walkthrough-creating-a-dataflow-pipeline#creating-the-dataflow-blocks
    /// </summary>
    class CreatingADataflowPipelineProgram
    {
        public static void Run()
        {
            //
            // Create the members of the pipeline.
            // 
            // Downloads the requested resource as a string.
            TransformBlock<string, string> downloadString = new TransformBlock<string, string>(async uri =>
            {
                Console.WriteLine("Downloading '{0}'...", uri);
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine();
                return await new HttpClient().GetStringAsync(uri);
            });

            // Separates the specified text into an array of words.
            TransformBlock<string, string[]> createWordList = new TransformBlock<string, string[]>(text =>
            {
                Console.WriteLine("Creating word list...");
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine();
                // Remove common punctuation by replacing all non-letter characters 
                // with a space character.
                char[] tokens = text.Select(c => char.IsLetter(c) ? c : ' ').ToArray();
                text = new string(tokens);

                // Separate the text into an array of words.
                return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            });

            // Removes short words and duplicates.
            TransformBlock<string[], string[]> filterWordList = new TransformBlock<string[], string[]>(words =>
            {
                Console.WriteLine("Filtering word list...");
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine();
                return words
                    .Where(word => word.Length > 3)
                    .Distinct()
                    .ToArray();
            });

            // Finds all words in the specified collection whose reverse also 
            // exists in the collection.
            TransformManyBlock<string[], string> findReversedWords = new TransformManyBlock<string[], string>(words =>
            {
                Console.WriteLine("Finding reversed words...");
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine();
                HashSet<string> wordsSet = new HashSet<string>(words);

                return from word in words.AsParallel()
                       let reverse = new string(word.Reverse().ToArray())
                       where word != reverse && wordsSet.Contains(reverse)
                       select word;
            });

            // Prints the provided reversed words to the console.    
            ActionBlock<string> printReversedWords = new ActionBlock<string>(reversedWord =>
            {
                Console.WriteLine("Found reversed words {0}/{1}", reversedWord, new string(reversedWord.Reverse().ToArray()));
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine();
            });

            //
            // Connect the dataflow blocks to form a pipeline.
            //

            DataflowLinkOptions linkOptions = new DataflowLinkOptions
            {
                // Gets or sets whether the linked target will have completion and
                // faulting notification propagated to it automatically
                PropagateCompletion = true
            };

            downloadString.LinkTo(createWordList, linkOptions);
            createWordList.LinkTo(filterWordList, linkOptions);
            filterWordList.LinkTo(findReversedWords, linkOptions);
            findReversedWords.LinkTo(printReversedWords, linkOptions);

            // Process "The Iliad of Homer" by Homer.
            /*This example uses DataflowBlock.Post to synchronously send data to the head
             of the pipeline. Use the DataflowBlock.SendAsync method when you must asynchronously 
             send data to a dataflow node.*/
            downloadString.Post("http://www.gutenberg.org/files/6130/6130-0.txt");

            /*Add the following code to mark the head of the pipeline as completed.
             The head of the pipeline propagates its completion after it processes 
             all buffered messages.*/
            /*Signals to the IDataflowBlock that it should not accept nor produce any more
             messages nor consume any more postponed messages*/
            downloadString.Complete();

            /*Add the following code to wait for the pipeline to finish. The overall operation is
             finished when the tail of the pipeline finishes.*/
            // Wait for the last block in the pipeline to process all messages.
            printReversedWords.Completion.Wait();

        }
    }
}
