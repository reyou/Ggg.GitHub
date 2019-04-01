using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GggDocsClassLibrary.Utilities
{
    public static class GggTestUtilities
    {
        private static readonly StringBuilder Builder = new StringBuilder();
        public static int Counter;
        public static string TestResultsPath = @"./Assets/TestResults.txt";
        public static void ShowOutput(object objectToSerialize)
        {
            Formatting settings = Formatting.Indented;
            string serializeObject = JsonConvert.SerializeObject(objectToSerialize, settings);
            ShowOutput(serializeObject);
        }
        public static void ShowText(string toString)
        {
            FileInfo fileInfo = new FileInfo(TestResultsPath);
            File.WriteAllText(fileInfo.FullName, "");
            File.WriteAllText(fileInfo.FullName, toString);
            Process.Start(fileInfo.FullName);
        }
        public static void ShowText<T>(T toString) where T : class
        {
            string serializeObject = JsonConvert.SerializeObject(toString, Formatting.Indented);
            FileInfo fileInfo = new FileInfo(TestResultsPath);
            File.WriteAllText(fileInfo.FullName, "");
            File.WriteAllText(fileInfo.FullName, serializeObject);
            Process.Start(fileInfo.FullName);
        }
        public static void ShowOutput(string contents = "")
        {

            const string fileName = "TestOutput.txt";
            // StackTrace stackTrace = new StackTrace();           // get call stack
            // StackFrame[] stackFrames = stackTrace.GetFrames();
            StringBuilder builder = new StringBuilder();
            //foreach (StackFrame stackFrame in stackFrames)
            //{
            //    builder.AppendLine(stackFrame.GetMethod().Name);
            //}
            // builder.AppendLine("=====================================");
            builder.AppendLine(contents);
            File.WriteAllText(fileName, builder.ToString());
            Process.Start(fileName);
        }
        public static void AppendOutput(string value)
        {
            Builder.AppendLine(value);
        }

        public static void Show()
        {
            ShowOutput(Builder.ToString());
        }

        public static void AppendLineSeparator()
        {
            Builder.AppendLine("");
            Builder.AppendLine("========================================================================");
            Builder.AppendLine("");
        }

        public static void AppendSerializedOutput(object field)
        {
            Builder.AppendLine(JsonConvert.SerializeObject(field));
            Builder.AppendLine("");
        }

        public static void AppendCounter(string prefix = "", string suffix = "")
        {
            Counter++;
            Builder.AppendLine(prefix + Counter + suffix + "-");
        }
        public static void OpenFile(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            Process.Start(fileInfo.FullName);
        }
    }
}
