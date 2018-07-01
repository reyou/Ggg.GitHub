using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Events;

namespace SerilogConsoleApp.WritingLogEvents
{
    /// <summary>
    /// In most cases, applications should write events without checking the active
    /// logging level. Level checking is extremely cheap and the overhead of calling
    /// disabled logger methods very low.
    /// In rare, performance-sensitive cases, the recommended pattern for level
    /// detection is to store the results of level detection in a field, for example:
    /// </summary>
    class LevelDetectionExample
    {
        public static void Run()
        {
            TestUtilities.SetupLogger();
            bool isDebug = Log.IsEnabled(LogEventLevel.Debug);
            if (isDebug)
            {
                Log.Debug("Someone is stuck debugging...");
            }
        }
    }
}
