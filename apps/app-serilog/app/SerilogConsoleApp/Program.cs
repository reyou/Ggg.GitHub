using System;
using System.Threading;
using SerilogConsoleApp.ConfigurationBasics;
using SerilogConsoleApp.FormattingOutput;
using SerilogConsoleApp.GettingStarted;
using SerilogConsoleApp.Github.SerilogSinksElasticsearch;
using SerilogConsoleApp.StructuredData;
using SerilogConsoleApp.WritingLogEvents;

namespace SerilogConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConfigurationBasics();
            // GettingStarted();
            // StructuredData();
            // WritingLogEvents();
            // FormattingOutput();
            SerilogSinksElasticsearch();
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("Main program end.");
            Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("===============================================");
            Console.ReadLine();
        }

        private static void SerilogSinksElasticsearch()
        {
            // SerilogSinksElasticsearchExamples.Run();
            // HandlingErrorsExamples.Run();
            GetStartedExamples.Run();
        }

        private static void FormattingOutput()
        {
            // FormattingPlainTextExamples.Run();
            FormattingJsonExamples.Run();
            // FormatProvidersExamples.Run();
            // FormatProvidersExamples.Run2();
        }

        private static void WritingLogEvents()
        {
            // WritingLogEventsExamples.Run();
            // LevelDetectionExample.Run();
            // DynamicLevelsExamples.Run();
            // SourceContextsExamples.Run();
            CorrelationExamples.Run();
        }

        private static void StructuredData()
        {
            // SimpleScalarValuesExample.Run();
            // CollectionsExample.Run();
            // ObjectsExample.Run();
            // PreservingObjectStructureExample.Run();
            // CustomizingTheStoredDataExample.Run();
            ForcingStringificationExample.Run();
        }

        private static void ConfigurationBasics()
        {
            // CreatingALogger.Run();
            // SinksExample.Run2();
            // OutputTemplatesExample.Run();
            // MinimumLevelExample.Run();
            // OverridingPerSinkExample.Run();
            // EnrichersExample.Run2();
            // FiltersExample.Run();
            SubLoggersExample.Run();
        }

        private static void GettingStarted()
        {
            ExampleApplication.Run();
        }
    }
}
