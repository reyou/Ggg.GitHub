using System;
using System.Threading;
using Newtonsoft.Json;

namespace ConsoleTaksRunner.ConsoleApp
{
    public class TestUtilities
    {
        public static void ConsoleWriteJson(object item)
        {
            Console.WriteLine();
            Console.WriteLine(JsonConvert.SerializeObject(new
            {
                Thread.CurrentThread.ManagedThreadId,
                ThreadName = Thread.CurrentThread.Name,
                item
            }, Formatting.Indented));
            Console.WriteLine();
        }
        public static void ThreadSleepSeconds(int seconds, string message = "")
        {
            Console.WriteLine($"{message} - ManagedThreadId:{Thread.CurrentThread.ManagedThreadId} " +
                              $"(Name:{Thread.CurrentThread.Name}) sleeps {seconds} sec...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
        public static void MethodEnds()
        {
            Console.WriteLine();
            Console.WriteLine("=========== Method Ends =================");
            Console.WriteLine();
        }
    }
}