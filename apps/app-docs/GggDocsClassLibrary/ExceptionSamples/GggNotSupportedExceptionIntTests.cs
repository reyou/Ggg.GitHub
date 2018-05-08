using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.ExceptionSamples
{
    [TestClass]
    public class GggNotSupportedExceptionIntTests
    {
        [TestMethod]
        public void DetectEncodingTest()
        {
            GggNotSupportedException.DetectEncoding();
        }
        [TestMethod]
        public void RunTest()
        {
            GggNotSupportedException.Run();
        }

    }
}