using GggDocsClassLibrary.Utilities;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GggDocsClassLibrary.ExceptionSamples
{
    /// <summary>
    /// NotSupportedException Class
    /// https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception?view=netframework-4.7.1
    /// </summary>
    public class GggNotSupportedException
    {
        public static void DetectEncoding()
        {
            try
            {
                String name = @".\TestFile.dat";
                FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write);
                Console.WriteLine("Filename: {0}, Encoding: {1}", name, FileUtilities.GetEncodingType(fs));
                GggTestUtilities.ShowOutput(new
                {
                    Filename = name,
                    Encoding = FileUtilities.GetEncodingType(fs)
                });
            }
            catch (NotSupportedException e)
            {
                GggTestUtilities.ShowOutput(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static void Run()
        {
            try
            {
                Encoding enc = Encoding.Unicode;
                String value = "This is a string to persist.";
                Byte[] bytes = enc.GetBytes(value);

                FileStream fs = new FileStream(@".\assets\readme.txt", FileMode.Open, FileAccess.Read);
                /*GetPreamble: When overridden in a derived class, returns a sequence of bytes 
             that specifies the encoding used.
             Returns A byte array containing a sequence of bytes that specifies 
             the encoding used.-or- A byte array of length zero, if a preamble is not required.*/
                // WriteAsync(byte[] buffer, int offset, int count)
                Task writeAsyncTask = fs.WriteAsync(enc.GetPreamble(), 0, enc.GetPreamble().Length);
                // WriteAsync(byte[] buffer, int offset, int count)
                Task continueWithTask = writeAsyncTask.ContinueWith(a => fs.WriteAsync(bytes, 0, bytes.Length));
                continueWithTask.Wait();
                fs.Close();

            }
            catch (NotSupportedException e)
            {
                GggTestUtilities.ShowOutput(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
