using System;
using System.Threading;
using Serilog;
using Serilog.Formatting.Compact;

namespace SerilogConsoleApp.WritingLogEvents
{
    internal class TestUtilities
    {
        public static void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static void SetupLoggerJson()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();
        }

        public static dynamic GetSampleDynamic()
        {
            var dynamicSample = new
            {
                Title = Guid.NewGuid().ToString().Substring(0, 5),
                Date = DateTime.UtcNow,
                Environment.MachineName,
                ThreadName = Thread.CurrentThread.Name
            };
            return dynamicSample;
        }
    }
}