using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Filters;

namespace SerilogConsoleApp.ConfigurationBasics
{
    /// <summary>
    /// Events can be selectively logged by filtering. Filters are just
    /// predicates over LogEvent, with some common scenarios handled by
    /// the Matching class.
    /// </summary>
    class FiltersExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Filter.ByExcluding(Matching.WithProperty<int>("Count", p => p < 10))
                .CreateLogger();
            Log.Information("FiltersExample: Events can be selectively logged by filtering.");
        }
    }
}
