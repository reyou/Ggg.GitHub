using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.ConfigurationBasics
{
    /// <summary>
    /// https://github.com/serilog/serilog/wiki/Configuration-Basics#sinks
    /// </summary>
    class SinksExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Ah, there you are!");
        }
        public static void Run2()
        {
            // Multiple sinks can be active at the same time.
            // Adding additional sinks is a simple as chaining WriteTo blocks:

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(@"c:\temp\logs\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Ah, there you are!");

        }
    }
}
