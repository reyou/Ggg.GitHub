using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Serilog;

namespace SerilogConsoleApp.FormattingOutput
{
    class FormatProvidersExamples
    {
        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }
        }

        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            User exampleUser = new User { Id = 1, Name = "Adam", Created = DateTime.Now };
            Log.Information("Created {@User} on {Created} {@Thread}", exampleUser, DateTime.Now, Thread.CurrentThread);

            Log.CloseAndFlush();
        }
        class CustomDateFormatter : IFormatProvider
        {
            readonly IFormatProvider basedOn;
            readonly string shortDatePattern;
            public CustomDateFormatter(string shortDatePattern, IFormatProvider basedOn)
            {
                this.shortDatePattern = shortDatePattern;
                this.basedOn = basedOn;
            }
            public object GetFormat(Type formatType)
            {
                if (formatType == typeof(DateTimeFormatInfo))
                {
                    var basedOnFormatInfo = (DateTimeFormatInfo)basedOn.GetFormat(formatType);
                    var dateFormatInfo = (DateTimeFormatInfo)basedOnFormatInfo.Clone();
                    dateFormatInfo.ShortDatePattern = this.shortDatePattern;
                    return dateFormatInfo;
                }
                return this.basedOn.GetFormat(formatType);
            }
        }

        public static void Run2()
        {
            CustomDateFormatter formatter = new CustomDateFormatter("dd-MMM-yyyy", new CultureInfo("en-AU"));
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(formatProvider: new CultureInfo("en-AU")) //Console1
                .WriteTo.Console(formatProvider: formatter)                //Console2
                .CreateLogger();

            User exampleUser = new User { Id = 1, Name = "Adam", Created = DateTime.Now };
            Log.Information("Created {@User} on {Created}", exampleUser, DateTime.Now);

            Log.CloseAndFlush();
        }
    }
}
