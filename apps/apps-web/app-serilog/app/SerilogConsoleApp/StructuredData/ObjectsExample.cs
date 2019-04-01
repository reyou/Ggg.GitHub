using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.StructuredData
{
    /// <summary>
    /// Apart from the types above, which are specially handled by
    /// Serilog, it is difficult to make intelligent choices about
    /// how data should be rendered and persisted. Objects not explicitly
    /// intended for serialisation tend to serialise very poorly.
    /// </summary>
    class ObjectsExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            var dynamicType =
                new
                {
                    Title = "computer",
                    UserName = "superman",
                    Color = "red"
                };
            Log.Information("This is superman: {superman}", dynamicType);
        }
    }
}
