using GggDocsClassLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GggDocsClassLibrary.FileStreamSamples
{
    /// <summary>
    /// <see cref="GggFileStream"/>
    /// </summary>
    [TestClass]
    public class GggFileStreamIntTests
    {
        [TestMethod]
        public async Task ReadFromAFileAsynchronouslyTest()
        {
            GggFileStream instance = new GggFileStream();
            string fileName = @"./Assets/ReadAsyncTest.txt";
            CancellationToken cancellationToken = new CancellationToken();
            Task<string> readFromAFileAsynchronously = instance.ReadFromAFileAsynchronously(fileName, cancellationToken);
            string text = await readFromAFileAsynchronously;
            GggTestUtilities.ShowText(text);
        }
        [TestMethod]
        public async Task WriteToAFileAsynchronouslyTest()
        {
            GggFileStream instance = new GggFileStream();
            string fileName = @"./Assets/WriteToAFileAsynchronouslyTest.txt";
            string sampleText = "Microsoft VisualStudio TestTools UnitTesting";
            await instance.WriteToAFileAsynchronously(fileName, sampleText);
            GggTestUtilities.OpenFile(fileName);
        }
        [TestMethod]
        public void CreateFileStreamTest()
        {
            GggFileStream instance = new GggFileStream();
            string path = @"./Assets/MyTest.txt";
            FileInfo fileInfo = new FileInfo(path);
            Process.Start(fileInfo.FullName);
            StringBuilder stringBuilder = instance.CreateFileStream(path);
            GggTestUtilities.ShowText(stringBuilder.ToString());
        }
    }
}