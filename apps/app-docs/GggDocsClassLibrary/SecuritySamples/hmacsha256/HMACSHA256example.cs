using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GggDocsClassLibrary.SecuritySamples.hmacsha256
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/system.security.cryptography.hmacsha256(v=vs.110).aspx
    /// </summary>
    public class Hmacsha256Example
    {
        public static void Main2(string[] Fileargs)
        {
            string dataFile;
            string signedFile;
            //If no file names are specified, create them.
            if (Fileargs == null || Fileargs.Length < 2)
            {
                dataFile = @"text.txt";
                signedFile = "signedFile.enc";

                if (!File.Exists(dataFile))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(dataFile))
                    {
                        sw.WriteLine("Here is a message to sign");
                    }
                }

            }
            else
            {
                dataFile = Fileargs[0];
                signedFile = Fileargs[1];
            }

            try
            {
                // Create a random key using a random number generator. This would be the
                //  secret key shared by sender and receiver.
                byte[] secretkey = new Byte[64];
                //RNGCryptoServiceProvider is an implementation of a random number generator.
                /*Implements a cryptographic Random Number Generator (RNG) using the
                 implementation provided by the cryptographic service provider 
                 (CSP). This class cannot be inherited*/
                // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rngcryptoserviceprovider?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(System.Security.Cryptography.RNGCryptoServiceProvider);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.6.1);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.7.2
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    // The array is now filled with cryptographically strong random bytes.
                    // Fills an array of bytes with a cryptographically strong sequence
                    // of random values
                    rng.GetBytes(secretkey);

                    // Use the secret key to sign the message file.
                    SignFile(secretkey, dataFile, signedFile);

                    // Verify the signed file
                    bool verifyFile = VerifyFile(secretkey, signedFile);
                    // A collection of helper classes to test various conditions
                    // within unit tests. If the condition being tested is not met,
                    // an exception is thrown
                    Assert.IsTrue(verifyFile);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error: File not found", e);
            }
        }

        // Compares the key in the source file with a new key created for the data portion of the file. If the keys 
        // compare the data has not been tampered with.
        private static bool VerifyFile(byte[] key, String sourceFile)
        {
            bool err = false;
            // Initialize the keyed hash object. 
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                // Create an array to hold the keyed hash value read from the file.
                byte[] storedHash = new byte[hmac.HashSize / 8];
                // Create a FileStream for the source file.
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    // Read in the storedHash.
                    inStream.Read(storedHash, 0, storedHash.Length);
                    // Compute the hash of the remaining contents of the file.
                    // The stream is properly positioned at the beginning of the content, 
                    // immediately after the stored hash value.
                    byte[] computedHash = hmac.ComputeHash(inStream);
                    // compare the computed hash with the stored value
                    for (int i = 0; i < storedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i])
                        {
                            err = true;
                        }
                    }
                }
            }
            if (err)
            {
                Console.WriteLine("Hash values differ! Signed file has been tampered with!");
                return false;
            }

            Console.WriteLine("Hash values agree -- no tampering occurred.");
            return true;
        }

        // Computes a keyed hash for a source file and creates a target file with the keyed hash
        // prepended to the contents of the source file. 
        private static void SignFile(byte[] key, String sourceFile, String destFile)
        {
            // Initialize the keyed hash object.
            // This constructor uses a 64-byte, randomly generated key.
            // The output hash is 256 bits in length
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(destFile, FileMode.Create))
                    {
                        // Compute the hash of the input file.
                        byte[] hashValue = hmac.ComputeHash(inStream);
                        // Reset inStream to the beginning of the file.
                        inStream.Position = 0;
                        // Write the computed hash value to the output file.
                        outStream.Write(hashValue, 0, hashValue.Length);
                        // Copy the contents of the sourceFile to the destFile.
                        int bytesRead;
                        // read 1K at a time
                        byte[] buffer = new byte[1024];
                        do
                        {
                            // Read from the wrapping CryptoStream.
                            bytesRead = inStream.Read(buffer, 0, 1024);
                            outStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
        }
    }
}
