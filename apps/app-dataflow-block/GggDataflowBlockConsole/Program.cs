// ReSharper disable RedundantUsingDirective
using GggDataflowBlockConsole.DataflowBlockReceivesData;
using System;
using System.Threading;
using GggDataflowBlockConsole.DataFlowMain;
using GggDataflowBlockConsole.DataFlowMain.ExecutionBlocks;
using GggDataflowBlockConsole.DataFlowMain.GroupingBlocks;

namespace GggDataflowBlockConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GroupingBlocks();
            // ExecutionBlocks();
            // BufferingBlocks();
            // DataflowMain();
            // ReadWriteMessages();
            // ProducerConsumerPattern();
            // DataflowBlockReceivesData();
            // CreatingDataflowPipeline();
            Console.WriteLine("Main thread id: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Main thread reached to end.");
            Console.ReadLine();
        }

        private static void GroupingBlocks()
        {
            // BatchBlockExample.Run();
            //JoinBlockExample.Run();
            BatchedJoinBlockExample.Run();
        }

        private static void ExecutionBlocks()
        {
            // ActionBlockExample.Run();
            // TransformBlockExample.Run();
            TransformManyBlockExample.Run();
        }

        private static void BufferingBlocks()
        {
            // DataflowBlockCompletion.Run();
            // BroadcastBlockExample.Run();
            // WriteOnceBlockExample.Run();
        }

        private static void DataflowMain()
        {
            // BufferBlockExample.RunAsync();


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
