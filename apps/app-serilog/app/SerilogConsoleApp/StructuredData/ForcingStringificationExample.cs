using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.StructuredData
{
    class ForcingStringificationExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            int[] unknown = { 1, 2, 3 };
            Log.Information("ForcingStringificationExample Data {Data}", unknown);
            Log.Information("ForcingStringificationExample @Data {@Data}", unknown);
            Log.Information("ForcingStringificationExample $Data {$Data}", unknown);
        }
    }
}
