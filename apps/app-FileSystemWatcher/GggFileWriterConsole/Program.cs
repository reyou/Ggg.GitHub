using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GggFileWriterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                Console.WriteLine("currentDirectory: " + currentDirectory);
                IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                Run(configuration);
                // Wait for the user to quit the program.
                Console.WriteLine("Press \'q\' to quit the sample.");
                while (Console.Read() != 'q')
                {

                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static void Run(IConfigurationRoot configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection("AppConfig").GetSection("FilePath");
            Console.WriteLine("FilePath: " + configurationSection.Value);
            CreateFileIfNotExists(configurationSection.Value);
            WriteText(configurationSection.Value);
        }

        private static void WriteText(string filePath)
        {
            void Function()
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    try
                    {
                        string content = i + "- " + Guid.NewGuid() + Environment.NewLine;
                        File.AppendAllText(filePath, content);
                        Console.WriteLine("Content written: " + content);
                        int random = (new Random()).Next(200, 1000);
                        System.Threading.Thread.Sleep(random);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
            }

            Task.Run((Action)Function);
            // task.Start();
        }

        private static void CreateFileIfNotExists(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Directory != null && !Directory.Exists(info.Directory.FullName))
            {
                Directory.CreateDirectory(info.FullName);
            }
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }
        }
    }
}
