using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace SerilogConsoleApp.ConfigurationBasics
{
    class EnrichersExample
    {
        /// <summary>
        /// Enrichers are simple components that add, remove or modify the
        /// properties attached to a log event. This can be used for the
        /// purpose of attaching a thread id to each event, for example.
        /// </summary>
        class ThreadIdEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                LogEventProperty logEventProperty = propertyFactory.CreateProperty(
                    "ThreadId", Thread.CurrentThread.ManagedThreadId);
                logEvent.AddPropertyIfAbsent(logEventProperty);
            }
        }

        /// <summary>
        /// Enrichers are added using the Enrich configuration object.
        /// </summary>
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
                .CreateLogger();

            Log.Information("EnrichersExample: Ah, there you are!");

        }

        /// <summary>
        /// If the enriched property value is constant throughout the application run,
        /// the shortcut WithProperty method can be used to simplify configuration.
        /// </summary>
        public static void Run2()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Version", "1.0.0")
                .WriteTo.Console()
                .CreateLogger();


            Log.Information("EnrichersExample: enriched property!");

        }
    }
}
