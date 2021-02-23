using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Carter.HtmlNegotiator.Tests.Stubs
{
    public class StubWebHostEnvironment : IWebHostEnvironment
    {
        public StubWebHostEnvironment()
        {
            ContentRootPath = String.Empty;
        }
        
        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string WebRootPath { get; set; }
    }
}