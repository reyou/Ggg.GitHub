using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

namespace SerilogConsoleApp.Github.SerilogSinksElasticsearch
{
    public class HandlingErrorsExamples
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                    FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null)
                }).CreateLogger();
        }
    }
}
