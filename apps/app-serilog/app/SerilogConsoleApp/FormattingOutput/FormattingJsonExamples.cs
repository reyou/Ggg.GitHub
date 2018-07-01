using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Formatting.Compact;
using SerilogConsoleApp.WritingLogEvents;

namespace SerilogConsoleApp.FormattingOutput
{
    class FormattingJsonExamples
    {
        public static void Run()
        {
            TestUtilities.SetupLoggerJson();
            dynamic sampleDynamic = TestUtilities.GetSampleDynamic();
            Log.Information("FormattingJsonExamples: This is a sample.");
            Log.Information("Sample Dynamic: {sampleDynamic}", sampleDynamic);
        }
    }
}
