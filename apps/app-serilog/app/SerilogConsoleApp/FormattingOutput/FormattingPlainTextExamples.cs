using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.FormattingOutput
{
    class FormattingPlainTextExamples
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            Log.Information("FormattingPlainTextExamples");

        }
    }
}
