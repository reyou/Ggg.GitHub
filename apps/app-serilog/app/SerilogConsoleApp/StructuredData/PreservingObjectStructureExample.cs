using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.StructuredData
{
    /// <summary>
    /// There are many places where, given the capability, it makes sense to
    /// serialise a log event property as a structured object.
    /// DTOs (data transfer objects), messages, events and models are
    /// often best logged by breaking them down into properties with values.
    /// For this task, Serilog provides the @ destructuring operator.
    /// </summary>
    class PreservingObjectStructureExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            var sensorInput = new
            {
                Latitude = 25,
                Longitude = 134,
                Country = new
                {
                    FlagColor = "red",
                    Capital = "Washington DC"
                }
            };
            Log.Information("Processing {SensorInput}", sensorInput);
            Log.Information("Processing Destructuring {@SensorInput}", sensorInput);

        }
    }
}
