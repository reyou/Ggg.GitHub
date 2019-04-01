using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.WritingLogEvents
{
    class SourceContextsExamples
    {
        public static void Run()
        {
            TestUtilities.SetupLogger();
            Log.ForContext<SourceContextsExamples>().Information("Hello {SourceContext}!");
        }
    }
}
