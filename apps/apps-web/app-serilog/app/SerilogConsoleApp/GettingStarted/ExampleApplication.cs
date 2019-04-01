using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.GettingStarted
{
    /// <summary>
    /// https://github.com/serilog/serilog/wiki/Getting-Started#example-application
    /// file:///C:/temp/logs/myapp20180630.txt
    /// </summary>
    class ExampleApplication
    {
        public static void Run()
        {
            // An optional static entry point for logging that can be easily referenced
            // by different parts of an application. To configure the <see cref="T:Serilog.Log" />
            // set the Logger static property to a logger instance.
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("C:\\temp\\logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Hello, world!");
            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }

            Log.CloseAndFlush();
        }
    }
}
