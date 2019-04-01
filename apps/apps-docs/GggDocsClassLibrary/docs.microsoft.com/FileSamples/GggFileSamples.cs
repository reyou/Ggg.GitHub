using System.Collections.Generic;
using System.IO;

namespace GggDocsClassLibrary.docs.microsoft.com.FileSamples
{
    public class GggFileSamples
    {
        public static List<string> GetFileList(string rootDir)
        {
            List<string> files = new List<string>();
            GetFileListRecursive(rootDir, files);
            return files;
        }

        /// <summary>
        /// Gets the file list recursive.
        /// Deep First Search
        /// </summary>
        /// <param name="rootDir">The root dir.</param>
        /// <param name="files">The files.</param>
        private static void GetFileListRecursive(string rootDir, List<string> files)
        {
            string[] strings = Directory.GetFiles(rootDir);
            files.AddRange(strings);
            string[] directories = Directory.GetDirectories(rootDir);
            if (directories.Length == 0)
            {
                return;
            }
            foreach (string directory in directories)
            {
                GetFileListRecursive(directory, files);
            }
        }
    }
}
