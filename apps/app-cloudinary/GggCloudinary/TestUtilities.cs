using System.Diagnostics;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;

namespace GggCloudinary
{
    public class TestUtilities
    {
        public static Cloudinary GetCloudinary()
        {
            string cloud = GetCloudName();
            string apiKey = GetApiKey();
            string apiSecret = GetApiSecret();
            Account account = new Account(cloud, apiKey, apiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }

        private static string GetApiSecret()
        {
            return File.ReadAllText(@"D:\apikeys\cloudinary\apiSecret.txt");
        }

        private static string GetApiKey()
        {
            return File.ReadAllText(@"D:\apikeys\cloudinary\apiKey.txt");
        }

        private static string GetCloudName()
        {
            return File.ReadAllText(@"D:\apikeys\cloudinary\cloud.txt");
        }

        public static void LogAndWrite(object objectItem, string fileName)
        {
            Trace.WriteLine(objectItem);
            string serializeObject = JsonConvert.SerializeObject(objectItem, Formatting.Indented);
            string path = @"D:\Git\Ggg.Github\Ggg.Csharp\apps\app-cloudinary\GggCloudinary\TestOutputs\" + fileName;
            File.WriteAllText(path, serializeObject);
        }

        public static string GetImagesFolder()
        {
            return @"D:\Git\Ggg.Github\Ggg.Csharp\apps\app-cloudinary\images";
        }
    }
}