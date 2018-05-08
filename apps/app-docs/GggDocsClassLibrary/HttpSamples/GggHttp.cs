using System;
using System.IO;
using System.Net.Http;

namespace GggDocsClassLibrary.HttpSamples
{
    /// <summary>
    /// <see cref="GggHttpIntTests"/>
    /// </summary>
    public class GggHttp
    {
        /// <summary>
        /// Gets the multipart form data HTTP request message.
        /// https://stackoverflow.com/questions/45044607/setting-a-content-disposition-http-header-in-web-api
        /// </summary>
        /// <returns></returns>
        public HttpRequestMessage GetMultipartFormDataHttpRequestMessage()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            MultipartFormDataContent main = new MultipartFormDataContent(Guid.NewGuid().ToString());
            // Part 1
            HttpContent content = new StringContent("image.jpg");
            content.Headers.Clear();
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            { Name = "fileName" };
            // add content
            main.Add(content);
            // Part 2
            content = new StreamContent(new MemoryStream(new byte[] { 1, 2, 3 }));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            { Name = "fileUpload", FileName = "image.jpg" };
            // add content
            main.Add(content);
            httpRequestMessage.Content = main;
            return httpRequestMessage;
        }
    }
}
