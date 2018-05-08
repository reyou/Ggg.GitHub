using GggDocsClassLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GggDocsClassLibrary.FileSamples
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
