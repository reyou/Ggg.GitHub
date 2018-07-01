using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace SerilogConsoleApp.WritingLogEvents
{
    /// <summary>
    /// https://github.com/serilog/serilog/wiki/Writing-Log-Events#dynamic-levels
    /// Many larger/distributed apps need to run at a fairly restricted level of logging,
    /// say, Information (my preference) or Warning, and only turn up the instrumentation
    /// to Debug or Verbose when a problem has been detected and the overhead of collecting
    /// a bit more data is justified
    /// </summary>
    class DynamicLevelsExamples
    {
        public static void Run()
        {
            TestUtilities.SetupLogger();
            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch();
            levelSwitch.MinimumLevel = LogEventLevel.Warning;
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.ControlledBy(levelSwitch)
                  .WriteTo.ColoredConsole()
                  .CreateLogger();
            /*Now, events written to the logger will be filtered according to the
             switch’s MinimumLevel property.
            To turn the level up or down at runtime, perhaps in response to a 
            command sent over the network, change the property:*/
            levelSwitch.MinimumLevel = LogEventLevel.Verbose;
            Log.Verbose("This will now be logged");

        }
    }
}
