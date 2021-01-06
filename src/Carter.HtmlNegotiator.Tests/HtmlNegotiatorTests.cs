using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Carter.HtmlNegotiator.Tests.Stubs;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class HtmlNegotiatorTests
    {
        private readonly HtmlNegotiator htmlNegotiator;

        public HtmlNegotiatorTests()
        {
            htmlNegotiator = new HtmlNegotiator(new StubViewLocator(), new StubViewEngine());
        }
        
        [Fact]
        public void Should_Be_Able_To_Handle_Requests_With_A_HTML_MediaType()
        {
            var headerValue = new MediaTypeHeaderValue("text/html");

            var result = this.htmlNegotiator.CanHandle(headerValue);

            Assert.True(result);
        }

        [Fact]
        public void Should_Not_Be_Able_To_Handle_Requests_Other_MediaTypes()
        {
            var headerValue = new MediaTypeHeaderValue("application/json");

            var result = this.htmlNegotiator.CanHandle(headerValue);

            Assert.False(result);
        }

        [Fact]
        public async Task Should_Return_A_HTML_Response()
        { 
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await this.htmlNegotiator.Handle(httpContext.Request, httpContext.Response, "Hello from Carter!", CancellationToken.None);
            
            Assert.Equal(200, httpContext.Response.StatusCode);
            Assert.Equal("text/html", httpContext.Response.ContentType);
            
            httpContext.Response.Body.Position = 0;
            using var streamReader = new StreamReader(httpContext.Response.Body);
            var actualResponseText = await streamReader.ReadToEndAsync();
            Assert.Contains("<h1>Hello from Carter!</h1>", actualResponseText);
        }
    }
}