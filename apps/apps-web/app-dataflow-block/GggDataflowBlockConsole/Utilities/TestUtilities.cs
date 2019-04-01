using System;
using System.IO;

namespace GggDataflowBlockConsole.DataFlowMain.GroupingBlocks
{
    internal class TestUtilities
    {
        public static void SetConsoleOutput(string fileName, string extension = ".txt")
        {
            FileInfo logFile = new FileInfo($@"C:\temp\logs\{fileName}{extension}");
            if (logFile.Directory != null && !logFile.Directory.Exists)
            {
                Directory.CreateDirectory(logFile.Directory.FullName);
            }
            FileStream fileStream = new FileStream(logFile.FullName, FileMode.OpenOrCreate);
            FileStream filestream = fileStream;
            StreamWriter streamwriter = new StreamWriter(filestream)
            {
                AutoFlush = true
            };
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
        }
    }
}