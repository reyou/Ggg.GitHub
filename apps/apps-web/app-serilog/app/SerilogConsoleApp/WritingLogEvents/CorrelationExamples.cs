using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.WritingLogEvents
{
    /// <summary>
    /// Correlation: one of the several measures of the linear statistical relationship
    /// between two random variables, indicating both the strength and direction
    /// of the relationship
    /// Just as ForContext<T>() tags log events with the class that wrote them,
    /// other overloads of ForContext() enable log events to be tagged with identifiers
    /// that later support correlation of the events written with that identifier.
    /// </summary>
    class CorrelationExamples
    {
        public static void Run()
        {
            TestUtilities.SetupLogger();
            JobGgg job = GetNextJob();
            ILogger jobLog = Log.ForContext("JobId", job.Id);
            jobLog.Information("Running a new job: {JobId}");
            job.Run();
            jobLog.Information("Finished: {JobId}");

        }

        private static JobGgg GetNextJob()
        {
            return new JobGgg()
            {
                Title = "Clean Garbage",
                Id = 1,
                IntervalSeconds = TimeSpan.FromDays(2).TotalSeconds
            };
        }
    }

    internal class JobGgg
    {
        public string Title { get; set; }
        public double IntervalSeconds { get; set; }
        public int Id { get; set; }

        public void Run()
        {
            Log.Information("Hey, I am running!");
        }
    }
}
