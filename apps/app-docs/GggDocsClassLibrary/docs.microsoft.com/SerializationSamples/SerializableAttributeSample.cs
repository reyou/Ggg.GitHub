using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace GggDocsClassLibrary.docs.microsoft.com.SerializationSamples
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute?view=netframework-4.7.1
    /// </summary>
    public class SerializableAttributeSample
    {
        public static void Run()
        {
            //Creates a new TestSimpleObject object.
            TestSimpleObject obj = new TestSimpleObject();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            //Opens a file and serializes the object into it in binary format.
            Stream stream = File.Open("data.xml", FileMode.Create);
            SoapFormatter formatter = new SoapFormatter();

            //BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);
            stream.Close();

            //Empties obj.
            obj = null;

            //Opens file "data.xml" and deserializes the object from it.
            stream = File.Open("data.xml", FileMode.Open);
            formatter = new SoapFormatter();

            //formatter = new BinaryFormatter();

            obj = (TestSimpleObject)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();
        }
    }
    // A test object that needs to be serialized.
}
