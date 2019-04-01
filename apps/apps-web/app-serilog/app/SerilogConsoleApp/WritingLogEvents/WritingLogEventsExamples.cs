using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.WritingLogEvents
{
    class WritingLogEventsExamples
    {
        public static void Run()
        {
            TestUtilities.SetupLogger();
            int quota = 100;
            string user = "GitSeri";
            Log.Warning("Disk quota {Quota} MB exceeded by {User}", quota, user);

        }
    }
}
