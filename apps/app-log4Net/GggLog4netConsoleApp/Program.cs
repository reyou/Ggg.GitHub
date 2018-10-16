using log4net;
using log4net.Config;
using System;

namespace GggUnitTestProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        public static void Log()
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            XmlConfigurator.Configure();
            log.Debug("log Debug");
            log.Info("log Info");
            log.Warn("log Warn");
            log.Error("log Error");
            log.Fatal("log Fatal");
        }
    }
}
