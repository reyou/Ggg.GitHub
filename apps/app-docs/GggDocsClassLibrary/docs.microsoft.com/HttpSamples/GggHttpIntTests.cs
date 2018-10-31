using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.docs.microsoft.com.HttpSamples
{
    /// <summary>
    /// <see cref="GggHttp"/>
    /// </summary>
    [TestClass]
    public class GggHttpIntTests
    {
        [TestMethod]
        public void GetMultipartFormDataHttpRequestMessageTest()
        {
            GggHttp instance = GetInstance();
            HttpRequestMessage multipartFormDataHttpRequestMessage = instance.GetMultipartFormDataHttpRequestMessage();
            Assert.IsNotNull(multipartFormDataHttpRequestMessage);
        }

        private GggHttp GetInstance()
        {
            GggHttp gggHttp = new GggHttp();
            return gggHttp;
        }
    }
}