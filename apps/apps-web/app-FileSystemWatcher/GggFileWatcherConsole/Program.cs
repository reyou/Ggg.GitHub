using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

namespace GggFileWatcherConsole
{
    class Program
    {
        public IConfiguration Configuration { get; private set; }
        private static long initialFileSize;
        private static long lastReadLength;
        /// <summary>
        /// https://blog.bitscry.com/2017/05/30/appsettings-json-in-net-core-console-app/
        /// https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                Run(configuration);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/42497394/929902
        /// </summary>
        /// <param name="configuration"></param>
        private static void Run(IConfigurationRoot configuration)
        {

            IConfigurationSection configurationSection = configuration.GetSection("AppConfig").GetSection("FilePath");
            CreateFileIfNotExists(configurationSection.Value);
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            FileInfo fileInfo = new FileInfo(configurationSection.Value);
            if (fileInfo.Directory == null)
            {
                throw new InvalidOperationException("Directory does not exists: " + fileInfo.FullName);
            }
            Console.WriteLine("[Watching]: " + fileInfo.FullName);
            initialFileSize = fileInfo.Length;
            lastReadLength = initialFileSize - 1024;
            if (lastReadLength < 0) lastReadLength = 0;

            watcher.Path = fileInfo.Directory.FullName;
            /* Watch for changes in LastAccess and LastWrite times, and
          the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = fileInfo.Name;
            // Add event handlers.
            watcher.Changed += OnChanged;
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q')
            {

            }
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

        // Define the event handlers.
        /// <summary>
        /// https://stackoverflow.com/questions/3791103/c-sharp-continuously-read-file
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            string filePath = e.FullPath;
            long fileSize = new FileInfo(filePath).Length;
            if (fileSize > lastReadLength)
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Seek(lastReadLength, SeekOrigin.Begin);
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        int bytesRead = fs.Read(buffer, 0, buffer.Length);
                        lastReadLength += bytesRead;
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        string text = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.Write(text);
                    }
                }
            }
        }
    }
}
