using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.docs.microsoft.com.ExceptionDocs
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