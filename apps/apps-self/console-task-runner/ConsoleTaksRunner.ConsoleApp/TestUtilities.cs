using System;
using Newtonsoft.Json;

namespace ConsoleTaksRunner.ConsoleApp
{
    public class TestUtilities
    {
        public static void ConsoleWriteJson(object item)
        {
            Console.WriteLine();
            Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            Console.WriteLine();
        }
    }
}