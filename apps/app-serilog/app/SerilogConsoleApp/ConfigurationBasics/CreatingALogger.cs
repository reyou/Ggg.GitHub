using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.ConfigurationBasics
{
    /// <summary>
    /// The example above will create a logger that does not record events anywhere.
    /// To see log events, a sink must be configured.
    /// </summary>
    class CreatingALogger
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();
            Log.Information("No one listens to me!");
        }
    }
}
