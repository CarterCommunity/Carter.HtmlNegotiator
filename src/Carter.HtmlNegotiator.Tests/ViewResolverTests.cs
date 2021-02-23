using System.Collections.Generic;
using Carter.HtmlNegotiator.Tests.Stubs;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class ViewResolverTests
    {
        [Fact]
        public void Should_Return_Null_When_View_Name_Is_Null()
        {
            var subject = new ViewResolver(new StubFileSystem(new Dictionary<string, string>()), new StubWebHostEnvironment(), new HtmlNegotiatorConfiguration(new List<string>()));

            var result  = subject.GetView(new DefaultHttpContext(), null);
            
            result.ShouldBeNull();
        }
        
        [Fact]
        public void Should_Return_Null_When_View_Name_Is_Empty()
        {
            var subject = new ViewResolver(new StubFileSystem(new Dictionary<string, string>()), new StubWebHostEnvironment(), new HtmlNegotiatorConfiguration(new List<string>()));

            var result  = subject.GetView(new DefaultHttpContext(), string.Empty);
            
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Use_View_Location_Conventions_To_Resolve_View()
        {
            var fileSystem = new StubFileSystem(new Dictionary<string, string>{ ["Views/Home/Index.hbs"] = "<div>Hello World!</div>" });
            var subject = new ViewResolver(fileSystem, new StubWebHostEnvironment(), new HtmlNegotiatorConfiguration(new[] { "Views/{Resource}/{View}" }));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/";
            
            var result  = subject.GetView(httpContext, "Index.hbs");
            
            result.ShouldNotBeNull();
            result.ShouldBe("<div>Hello World!</div>");
        }

        [Theory]
        [InlineData("/Products", "Index.hbs", "<div>Index</div>")]
        [InlineData("/Orders/Checkout", "Checkout.hbs", "<div>Checkout</div>")]
        [InlineData("/Echo", "Echo.hbs", "<div>Echo</div>")]
        public void Should_Be_Able_To_Resolve_Resource_Name_From_Request_Path(string requestPath, string viewName, string expected)
        {
            var fileSystem = new StubFileSystem(new Dictionary<string, string>
            {
                ["Views/Home/Echo.hbs"] = "<div>Echo</div>",
                ["Views/Products/Index.hbs"] = "<div>Index</div>",
                ["Views/Orders/Checkout.hbs"] = "<div>Checkout</div>"
            });
            
            var locationConventions = new []
            {
                "Views/{Resource}/{View}"
            };
            
            var context = new DefaultHttpContext();
            context.Request.Path = requestPath;
            
            var subject = new ViewResolver(fileSystem, new StubWebHostEnvironment(), new HtmlNegotiatorConfiguration(locationConventions));
            
            var result  = subject.GetView(context, viewName);
            
            result.ShouldNotBeNull();
            result.ShouldBe(expected);
        }
        
        [Fact]
        public void Should_Be_Throw_An_AmbiguousViewsException_When_Multiple_View_Are_Found()
        {
            var fileSystem = new StubFileSystem(new Dictionary<string, string>
            {
                ["Features/Products/Index.hbs"] = string.Empty,
                ["Views/Products/Index.hbs"] = string.Empty
            });
            
            var locationConventions = new []
            {
                "Features/{Resource}/{View}",
                "Views/{Resource}/{View}"
            };
            
            var context = new DefaultHttpContext();
            context.Request.Path = "/Products";
            
            var subject = new ViewResolver(fileSystem, new StubWebHostEnvironment(), new HtmlNegotiatorConfiguration(locationConventions));

            var ex = Should.Throw<AmbiguousViewsException>(() => subject.GetView(context, "Index.hbs"));
            
            ex.Message.ShouldContain("Views (2)");
        }
        
        [Fact]
        public void Should_Be_Throw_An_ViewNotFoundException_When_No_Views_Are_Found()
        {
            var fileSystem = new StubFileSystem(new Dictionary<string, string>());
            var subject = new ViewResolver(fileSystem, new StubWebHostEnvironment(), new HtmlNegotiatorConfiguration(new List<string>()));

            var context = new DefaultHttpContext();
            context.Request.Path = "/Products";

            var ex = Should.Throw<ViewNotFoundException>(() => subject.GetView(context, "Index.hbs"));
            
            ex.Message.ShouldContain("The view 'Index.hbs' was not found");
        }
    }
}