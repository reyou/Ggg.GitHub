using System;
using System.IO;
using System.Threading.Tasks;

namespace GggDocsClassLibrary.ExceptionSamples
{
    public class FileUtilities
    {
        public enum EncodingType
        {
            None = 0,
            Unknown = -1,
            Utf8 = 1,
            Utf16 = 2,
            Utf32 = 3
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception?view=netframework-4.7.1
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static EncodingType GetEncodingType(FileStream fs)
        {
            /*You can eliminate the exception by examining the value of the
             FileStream.CanRead property and exiting the method if the 
             stream is read-only.*/
            if (!fs.CanRead)
            {
                return EncodingType.Unknown;
            }

            Byte[] bytes = new Byte[4];
            Task<int> t = fs.ReadAsync(bytes, 0, 4);
            t.Wait();
            int bytesRead = t.Result;
            if (bytesRead < 2)
                return EncodingType.None;

            if (bytesRead >= 3 & (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF))
                return EncodingType.Utf8;

            if (bytesRead == 4)
            {
                uint value = BitConverter.ToUInt32(bytes, 0);
                if (value == 0x0000FEFF | value == 0xFEFF0000)
                {
                    return EncodingType.Utf32;
                }
            }
            ushort value16 = BitConverter.ToUInt16(bytes, 0);
            if (value16 == 0xFEFF | value16 == 0xFFFE)
            {
                return EncodingType.Utf16;
            }

            return EncodingType.Unknown;
        }
    }
}
