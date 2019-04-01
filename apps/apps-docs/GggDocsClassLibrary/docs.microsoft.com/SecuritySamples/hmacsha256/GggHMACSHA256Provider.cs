using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace GggDocsClassLibrary.docs.microsoft.com.SecuritySamples.hmacsha256
{
    public class GggHmacsha256Provider
    {
        public string TimeStamp { get; set; }
        public string CreateHashCustom(string clientId, string secret)
        {
            byte[] key = Encoding.UTF8.GetBytes(secret);
            string timestamp = TimeStamp;
            if (string.IsNullOrEmpty(TimeStamp))
            {
                timestamp = GetEpoch().ToString();
            }
            string valueToHash = $"{{timestamp:{timestamp},client_id:{clientId}}}";

            // Initialize the keyed hash object.
            // This constructor uses a 64-byte, randomly generated key.
            // The output hash is 256 bits in length
            try
            {
                using (HMACSHA256 hmac = new HMACSHA256(key))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(valueToHash);
                    byte[] computeHash = hmac.ComputeHash(bytes);
                    string base64String = Convert.ToBase64String(computeHash);
                    return base64String;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private int GetEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch;
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }



    }
}
