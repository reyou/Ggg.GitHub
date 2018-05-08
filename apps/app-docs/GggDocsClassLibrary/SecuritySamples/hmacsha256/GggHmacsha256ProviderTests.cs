using GggDocsClassLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.SecuritySamples.hmacsha256
{
    [TestClass]
    public class GggHmacsha256ProviderTests
    {
        [TestMethod]
        public void MainTest()
        {

            GggHmacsha256Provider provider = new GggHmacsha256Provider();
            provider.TimeStamp = "1525810763201";
            string clientId = "28c8b711-b6b8-440c-9e78-d56a1bcc4aa2";
            string secret = "9d75e92b-11e9-45e0-88e2-6602869dfb82";
            string hash = provider.CreateHashCustom(clientId, secret);
            Assert.IsNotNull(hash);
            GggTestUtilities.ShowOutput(hash);
        }
    }
}