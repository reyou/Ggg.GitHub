using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GggDocsClassLibrary.FileStreamSamples
{
    /// <summary>
    /// <see cref="GggFileStreamIntTests"/>
    /// </summary>
    public class GggFileStream
    {
        /// <summary>
        /// Creates the file stream.
        /// </summary>
        public StringBuilder CreateFileStream(string path)
        {
            // Provides a Stream for a file, supporting both synchronous and asynchronous read and write operations.
            // https://msdn.microsoft.com/en-us/library/system.io.filestream(v=vs.110).aspx

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            //Create the file.
            using (FileStream fileStream = File.Create(path))
            {
                AddText(fileStream, "This is some text");
                AddText(fileStream, "This is some more text,");
                AddText(fileStream, "\r\nand this is on a new line");
                AddText(fileStream, $"\r\n\r\nThe following is a subset of characters:\r\n");

                for (int i = 1; i < 120; i++)
                {
                    AddText(fileStream, Convert.ToChar(i).ToString());
                }
            }
            //Open the stream and read it back.
            StringBuilder builder = new StringBuilder();
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] buffer = new byte[1024];
                UTF8Encoding utf8Encoding = new UTF8Encoding(true);
                while (fileStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    string s = utf8Encoding.GetString(buffer);
                    builder.Append(s);
                }
            }
            return builder;
        }

        /// <summary>
        /// Adds the text.
        /// </summary>
        /// <param name="fileStream">The fs.</param>
        /// <param name="value">The value.</param>
        private static void AddText(FileStream fileStream, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fileStream.Write(info, 0, info.Length);
        }

        public async Task WriteToAFileAsynchronously(string fileName, string textToWrite)
        {
            UnicodeEncoding uniencoding = new UnicodeEncoding();
            byte[] result = uniencoding.GetBytes(textToWrite);
            using (FileStream stream = File.Open(fileName, FileMode.OpenOrCreate))
            {
                // Specifies the position in a stream to use for seeking.
                stream.Seek(0, SeekOrigin.End);
                // Asynchronously writes a sequence of bytes to the current stream and advances
                // the current position within this stream by the number of bytes written.
                await stream.WriteAsync(result, 0, result.Length);
            }
        }

        /// <summary>
        /// Reads from a file asynchronously.
        /// https://blogs.msdn.microsoft.com/pfxteam/2012/10/05/how-do-i-cancel-non-cancelable-async-operations/
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<string> ReadFromAFileAsynchronously(string fileName, CancellationToken cancellationToken)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (FileStream stream = File.OpenRead(fileName))
            {
                UTF8Encoding utf8Encoding = new UTF8Encoding(true);

                byte[] buffer = new byte[256];
                // Asynchronously reads a sequence of bytes from the current stream, advances the
                // position within the stream by the number of bytes read, and monitors cancellation
                // requests.
                // Returns:
                // A task that represents the asynchronous read operation. The value of the TResult
                // parameter contains the total number of bytes read into the buffer. The result
                // value can be less than the number of bytes requested if the number of bytes currently
                // available is less than the requested number, or it can be 0 (zero) if the end
                // of the stream has been reached.
                try
                {
                    // So, can you cancel non-cancelable operations? No.  
                    // Can you cancel waits on non-cancelable operations?  
                    // Sure… just be very careful when you do.
                    while (await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken) > 0)
                    {
                        string bufferString = utf8Encoding.GetString(buffer);
                        stringBuilder.Append(bufferString);
                    }
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
            return stringBuilder.ToString();
        }
    }
}
