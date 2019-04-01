using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SerilogConsoleApp.StructuredData
{
    /// <summary>
    /// https://github.com/serilog/serilog/wiki/Structured-Data#collections
    /// </summary>
    class CollectionsExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            string[] fruit = { "Apple", "Pear", "Orange" };
            Log.Information("In my bowl I have {Fruit}", fruit);
            Dictionary<string, int> fruit2 = new Dictionary<string, int> { { "Apple", 1 }, { "Pear", 5 } };
            Log.Information("In my bowl I have {Fruit}", fruit2);

        }
    }
}
