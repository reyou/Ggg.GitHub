using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace GggDocsClassLibrary.standard.datetime
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/standard/datetime/converting-between-datetime-and-offset
    /// </summary>
    [TestClass]
    public class ConvertingBetweenDatetimeAndOffset
    {
        [TestMethod]
        public void Convert1()
        {
            DateTime utcTime1 = new DateTime(2008, 6, 19, 7, 0, 0);
            utcTime1 = DateTime.SpecifyKind(utcTime1, DateTimeKind.Utc);
            DateTimeOffset utcTime2 = utcTime1;
            string format = string.Format("Converted {0} {1} to a DateTimeOffset value of {2}",
                utcTime1,
                utcTime1.Kind.ToString(),
                utcTime2);
            Debug.WriteLine(format);
            DateTimeOffset dateTimeOffset = utcTime2.ToLocalTime();
            string localTime = dateTimeOffset.ToString("G");
            Debug.WriteLine(localTime);
        }

    }
}
