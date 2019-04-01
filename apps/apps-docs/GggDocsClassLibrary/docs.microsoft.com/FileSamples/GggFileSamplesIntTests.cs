using System.Collections.Generic;
using GggDocsClassLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.docs.microsoft.com.FileSamples
{
    [TestClass]
    public class GggFileSamplesIntTests
    {
        [TestMethod]
        public void GetFileListTest()
        {
            List<string> fileList = GggFileSamples.GetFileList("C:\\github");
            GggTestUtilities.ShowOutput(fileList);
        }
    }
}
