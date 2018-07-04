using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;
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
            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            LoggerConfiguration loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    IndexFormat = "foodceptionlogs-{0:yyyy.MM.dd}",
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback
                });

            Logger logger = loggerConfig.CreateLogger();
            logger.Error(new Exception("test"), "An error has occurred.");
            logger.Information("The {User} has just executed {Action}.", "username", "actionName");

        }
    }
}
