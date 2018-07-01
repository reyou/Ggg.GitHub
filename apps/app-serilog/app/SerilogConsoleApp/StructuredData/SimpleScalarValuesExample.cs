using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.StructuredData
{
    class SimpleScalarValuesExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            int count = 456;
            Log.Information("Retrieved {Count} records", count);

        }
    }
}
