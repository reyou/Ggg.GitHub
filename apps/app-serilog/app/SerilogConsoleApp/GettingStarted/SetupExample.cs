using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;

namespace SerilogConsoleApp.GettingStarted
{
    /// <summary>
    /// https://github.com/serilog/serilog/wiki/Getting-Started#setup
    /// </summary>
    public class SetupExample
    {
        /// <summary>
        /// This is typically done once at application start-up, and the logger
        /// saved for later use by application classes. Multiple loggers can be
        /// created and used independently if required.
        /// </summary>
        public static void Run()
        {
            // An ILogger is created using LoggerConfiguration.
            Logger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            log.Information("Hello, Serilog!");
        }
        public static void Run2()
        {
            // An ILogger is created using LoggerConfiguration.
            Logger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            Log.Logger = log;
            Log.Information("The global logger has been configured");
        }
    }
}
