using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Events;

namespace SerilogConsoleApp.ConfigurationBasics
{
    class OverridingPerSinkExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("C:\\temp\\logs\\log.txt")
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
            Log.Information("OverridingPerSinkExample: Ah, there you are!");
        }
    }
}
