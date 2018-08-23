namespace HtmlNegotiator.Tests
{
    using System;
    using System.Collections.Generic;
    using Carter;
    using Carter.HtmlNegotiator;
    using FakeItEasy;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    public class ViewRendererTests
    {
        private IViewResolver resolver;
        private IViewEngine engine;
        private HttpContext context;

        public ViewRendererTests()
        {
            var hostingEnvironment = A.Fake<IHostingEnvironment>();
            var serviceProvider = A.Fake<IServiceProvider>();
            this.context = A.Fake<HttpContext>();
            this.engine = A.Fake<IViewEngine>();
            this.resolver = A.Fake<IViewResolver>();
            
            A.CallTo(() => hostingEnvironment.ContentRootPath).Returns("some/path");
            A.CallTo(() => serviceProvider.GetService(typeof(IHostingEnvironment))).Returns(hostingEnvironment);
            A.CallTo(() => this.context.Items).Returns(new Dictionary<object, object> {{"ModuleType", typeof(CarterModule)}});
            A.CallTo(() => this.context.RequestServices).Returns(serviceProvider);
        }
        
        [Fact]
        public void Should_Render_A_Resolved_View_As_A_String()
        {
            // ARRANGE
            A.CallTo(() => this.resolver.ResolveView(A<ViewLocationContext>.Ignored)).Returns(new ViewTemplate{ Extension = "hbs", Source = ()=>"Source"});
            A.CallTo(() => this.engine.SupportedExtensions).Returns(new List<string>{ "hbs" });
            A.CallTo(() => this.engine.Render(A<ViewTemplate>.Ignored, null)).Returns("HTML");
            
            var viewRenderer = new ViewRenderer(this.resolver, new List<IViewEngine> { this.engine});
            
            // ACT
            var view = viewRenderer.RenderView(this.context, null);
            
            // ASSERT
            Assert.NotNull(view);
        }
        
        [Fact]
        public void Should_Return_Null_When_No_View_Is_Resolved()
        {
            // ARRANGE
            A.CallTo(() => this.resolver.ResolveView(A<ViewLocationContext>.Ignored)).Returns(null);
            
            var viewRenderer = new ViewRenderer(this.resolver, new List<IViewEngine> { this.engine});
            
            // ACT
            var view = viewRenderer.RenderView(this.context, null);
            
            // ASSERT
            Assert.Null(view);
        }
    }
}