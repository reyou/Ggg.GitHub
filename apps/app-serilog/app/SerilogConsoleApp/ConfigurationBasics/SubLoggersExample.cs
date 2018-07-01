using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Filters;

namespace SerilogConsoleApp.ConfigurationBasics
{
    /// <summary>
    /// Sometimes a finer level of control over what is seen by a
    /// sink is necessary. For this, Serilog allows a full logging
    /// pipeline to act as a sink
    /// </summary>
    class SubLoggersExample
    {
        /// <summary>
        /// For scenarios not handled well by sub-loggers, it's fine to create multiple
        /// independent top-level pipelines. Only one pipeline can be assigned to Log.Logger,
        /// but your app can use as many additional ILogger instances as it requires.
        /// </summary>
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.Logger(lc => lc
                        .Filter.ByExcluding(Matching.WithProperty<int>("Count", p => p < 10))
                    .WriteTo.File("C:\\temp\\logs\\SubLoggersExample.txt"))
                .CreateLogger();

            Log.Information("SubLoggersExample: Sometimes a finer level of control over what is seen.");

        }
    }
}
