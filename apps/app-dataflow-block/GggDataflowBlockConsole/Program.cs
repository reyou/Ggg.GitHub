// ReSharper disable RedundantUsingDirective
using System;
using System.Threading;
using GggDataflowBlockConsole.ActionBlockSample;
using GggDataflowBlockConsole.DataflowBlockReceivesData;
using GggDataflowBlockConsole.ProducerConsumerPattern;

namespace GggDataflowBlockConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // ReadWriteMessages();
            // ProducerConsumerPattern();
            // DataflowBlockReceivesData();
            CreatingDataflowPipeline();
            Console.WriteLine("Main thread id: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Main thread reached to end.");
            Console.ReadLine();
        }

        private static void CreatingDataflowPipeline()
        {
            throw new NotImplementedException();
        }

        private static void DataflowBlockReceivesData()
        {
            // ActionBlockSampleClass.Run();
            // DataflowExecutionBlocks.Run();
            DataflowExecutionBlocksAsync.Run();
        }

        private static void ProducerConsumerPattern()
        {
            // DataflowProducerConsumer.Run();
            // RobustProgramming.Run();
        }

        private static void ReadWriteMessages()
        {
            // WritingToAndReading.Run();
            // WritingToAndReading.RunWithTryReceive();
            // WritingToAndReadingSync.PostMethodActsSynchronously();
            // WritingToAndReadingASync.Run().Wait();
            // CompleteExample.Run();
        }
    }


}
