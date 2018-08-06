namespace HtmlNegotiator.Tests
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Carter.HtmlNegotiator;
    using FakeItEasy;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;
    using Xunit;

    public class HtmlNegotiatorTests
    {
        private readonly IViewRenderer viewRenderer;
        private readonly HtmlNegotiator htmlNegotiator;

        public HtmlNegotiatorTests()
        {
            viewRenderer = A.Fake<IViewRenderer>();
            htmlNegotiator = new HtmlNegotiator(viewRenderer);
        }

        [Fact]
        public void Should_Be_Able_To_Handle_Requests_With_A_HTML_MediaType()
        {
            // ARRANGE
            var headerValue = new MediaTypeHeaderValue("text/html");
            
            // ACT
            var result = this.htmlNegotiator.CanHandle(headerValue);
            
            // ASSERT
            Assert.True(result);
        }
        
        [Fact]
        public void Should_Not_Be_Able_To_Handle_Requests_Other_MediaTypes()
        {
            // ARRANGE
            var headerValue = new MediaTypeHeaderValue("application/not-html");
            
            // ACT
            var result = this.htmlNegotiator.CanHandle(headerValue);
            
            // ASSERT
            Assert.False(result);
        }
        
        [Fact]
        public async Task Should_Write_A_HTML_Response_When_A_View_Has_Been_Rendered()
        {
            // ARRANGE
            var httpContext = A.Fake<HttpContext>();
            var httpResponse = A.Fake<HttpResponse>();
            var httpRequest = A.Fake<HttpRequest>();
            
            A.CallTo(() => httpResponse.Body).Returns(new MemoryStream());
            A.CallTo(() => httpRequest.HttpContext).Returns(httpContext);
            A.CallTo(() => viewRenderer.RenderView(A<HttpContext>.Ignored, A<object>.Ignored)).Returns("Some HTML");
            
            // ACT
            await this.htmlNegotiator.Handle(httpRequest, httpResponse, null, CancellationToken.None);
            
            // ASSERT
            Assert.Equal(200, httpResponse.StatusCode);
            Assert.Equal("text/html", httpResponse.ContentType);
            Assert.True(httpResponse.Body.Length > 0, "Content length should be greater than 0");
        }
        
        [Fact]
        public async Task Should_Write_An_Error_Response_When_A_View_Has_Not_Been_Located()
        {
            // ARRANGE
            var httpContext = A.Fake<HttpContext>();
            var httpResponse = A.Fake<HttpResponse>();
            var httpRequest = A.Fake<HttpRequest>();
            
            A.CallTo(() => httpResponse.Body).Returns(new MemoryStream());
            A.CallTo(() => httpRequest.HttpContext).Returns(httpContext);
            A.CallTo(() => viewRenderer.RenderView(httpContext, A<object>.Ignored)).Returns(null);
            
            // ACT
            await this.htmlNegotiator.Handle(httpRequest, httpResponse, null, CancellationToken.None);
            
            // ASSERT
            Assert.Equal(500, httpResponse.StatusCode);
            Assert.Equal("text/plain", httpResponse.ContentType);
            Assert.True(httpResponse.Body.Length > 0, "Content length should be greater than 0");
        }
    }
}