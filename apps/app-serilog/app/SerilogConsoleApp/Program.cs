using System;
using System.Threading;
using SerilogConsoleApp.ConfigurationBasics;
using SerilogConsoleApp.GettingStarted;

namespace SerilogConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBasics();
            //GettingStarted();
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("Main program end.");
            Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("===============================================");
            Console.ReadLine();
        }

        private static void ConfigurationBasics()
        {
            // CreatingALogger.Run();
            // SinksExample.Run2();
            // OutputTemplatesExample.Run();
            // MinimumLevelExample.Run();
            OverridingPerSinkExample.Run();
        }

        private static void GettingStarted()
        {
            ExampleApplication.Run();
        }
    }
}
