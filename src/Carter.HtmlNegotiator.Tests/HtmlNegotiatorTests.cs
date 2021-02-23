using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Carter.HtmlNegotiator.Tests.Stubs;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Shouldly;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class HtmlNegotiatorTests
    {
        [Fact]
        public void Should_Be_Able_To_Handle_Requests_With_A_HTML_MediaType()
        {
            var headerValue = new MediaTypeHeaderValue("text/html");
            var htmlNegotiator = new HtmlNegotiator(new StubViewEngine());

            var result = htmlNegotiator.CanHandle(headerValue);

            result.ShouldBeTrue();
        }

        [Fact]
        public void Should_Not_Be_Able_To_Handle_Requests_Other_MediaTypes()
        {
            var headerValue = new MediaTypeHeaderValue("application/json");
            var htmlNegotiator = new HtmlNegotiator(new StubViewEngine());

            var result = htmlNegotiator.CanHandle(headerValue);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task Should_Return_A_HTML_Response_When_A_View_Has_Been_Found()
        { 
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();
            var htmlNegotiator = new HtmlNegotiator(new StubViewEngine());            

            await htmlNegotiator.Handle(httpContext.Request, httpContext.Response, "Hello from Carter!", CancellationToken.None);
            
            httpContext.Response.StatusCode.ShouldBe(200);
            httpContext.Response.ContentType.ShouldBe("text/html");
            
            httpContext.Response.Body.Position = 0;
            using var streamReader = new StreamReader(httpContext.Response.Body);
            var actualResponseText = await streamReader.ReadToEndAsync();
            actualResponseText.ShouldContain("<h1>Hello from Carter!</h1>");
        }
        
        [Fact]
        public async Task Should_Return_A_404_Response_When_A_View_Has_Not_Been_Found()
        { 
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/not-found";
            httpContext.Response.Body = new MemoryStream();
            var htmlNegotiator = new HtmlNegotiator(new StubViewEngine(true));

            await htmlNegotiator.Handle(httpContext.Request, httpContext.Response, "Hello from Carter!", CancellationToken.None);
            
            httpContext.Response.StatusCode.ShouldBe(404);
            httpContext.Response.ContentType.ShouldBe("text/plain");
            
            httpContext.Response.Body.Position = 0;
            using var streamReader = new StreamReader(httpContext.Response.Body);
            var actualResponseText = await streamReader.ReadToEndAsync();
            
            actualResponseText.ShouldContain("The view 'not-found.hbs' was not found.");
        }
    }
}