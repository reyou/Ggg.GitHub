using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.ConfigurationBasics
{
    class MinimumLevelExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("MinimumLevelExample: Ah, there you are!");
        }
    }
}
