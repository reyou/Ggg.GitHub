using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace SerilogConsoleApp.Github.SerilogSinksElasticsearch
{
    /// <summary>
    /// https://github.com/serilog/serilog-sinks-elasticsearch
    /// </summary>
    class SerilogSinksElasticsearchExamples
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
                }).CreateLogger();

        }

        /// <summary>
        /// https://github.com/serilog/serilog-sinks-elasticsearch#a-note-about-kibana
        /// </summary>
        public static void Run2()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
                }).CreateLogger();

        }
    }
}
