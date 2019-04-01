using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace SerilogConsoleApp.StructuredData
{
    /// <summary>
    /// Often only a selection of properties on a complex object are of interest.
    /// To customise how Serilog persists a destructured complex type, use the
    /// Destructure configuration object on LoggerConfiguration
    /// </summary>
    class CustomizingTheStoredDataExample
    {
        public static void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .Destructure.ByTransforming<HttpRequest>(
                    r => new { r.IsHttps, r.Method })
                .Destructure.ByTransforming<HttpRequestFake>(
                    r => new { r.IsHttps, r.Method })
                .WriteTo.Console().CreateLogger();
            HttpRequestFake httpRequest = new HttpRequestFake
            {
                IsHttps = true,
                Method = "GET"
            };
            Log.Information("CustomizingTheStoredDataExample: {@httpRequest}", httpRequest);

        }
    }


    internal class HttpRequestFake : HttpRequest
    {
        public override async Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public override HttpContext HttpContext { get; }
        public override string Method { get; set; }
        public override string Scheme { get; set; }
        public override bool IsHttps { get; set; }
        public override HostString Host { get; set; }
        public override PathString PathBase { get; set; }
        public override PathString Path { get; set; }
        public override QueryString QueryString { get; set; }
        public override IQueryCollection Query { get; set; }
        public override string Protocol { get; set; }
        public override IHeaderDictionary Headers { get; }
        public override IRequestCookieCollection Cookies { get; set; }
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override Stream Body { get; set; }
        public override bool HasFormContentType { get; }
        public override IFormCollection Form { get; set; }
    }
}
