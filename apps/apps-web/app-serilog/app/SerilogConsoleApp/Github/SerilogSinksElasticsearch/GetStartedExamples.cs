using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

namespace SerilogConsoleApp.Github.SerilogSinksElasticsearch
{
    /// <summary>
    /// https://github.com/serilog/serilog-sinks-elasticsearch
    /// The above code will register one sink, the Elasticsearch sink,
    /// with a reference to the ES server located on the localhost and port 9200.
    /// We also set the option AutoRegisterTemplate to true.
    /// This will create the correct mapping for us at startup.
    /// </summary>
    class GetStartedExamples
    {
        public static void Run()
        {
            Serilog.Debugging.SelfLog.Enable(delegate (string errorMessage)
            {
                Console.Write("Serilog.Debugging.SelfLog:" + errorMessage);
                Debug.Write("Serilog.Debugging.SelfLog:" + errorMessage);
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            });
            LoggerConfiguration loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    FailureCallback = e =>
                    {
                        Console.WriteLine("Unable to submit event " + e.MessageTemplate);
                        Debug.WriteLine("Unable to submit event " + e.MessageTemplate);
                        if (Debugger.IsAttached)
                        {
                            Debugger.Break();
                        }
                    },
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    // CustomFormatter = new CompactJsonFormatter(),
                    IndexFormat = "foodceptionlogs-{0:yyyy.MM.dd}",
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback
                });

            Logger logger = loggerConfig.CreateLogger();
            logger.Error(new Exception("test"), "An error has occurred.");
            User user = new User()
            {
                Title = "Doctor",
                UserName = "Shopping"
            };
            ActionExample action = new ActionExample()
            {
                Title = "Doctor",
                ActionColor = "RED"
            };
            logger.Information("The {@User} has just executed {@Action} at {Date}.", user, action, DateTime.Now);
            logger.Information("The {@User2} has just executed {@Action} at {Date}.", user, action, DateTime.Now);

        }
    }

    internal class ActionExample
    {
        public string ActionColor { get; set; }
        public string Title { get; set; }
    }

    internal class User
    {
        public string UserName { get; set; }
        public string Title { get; set; }
    }
}
