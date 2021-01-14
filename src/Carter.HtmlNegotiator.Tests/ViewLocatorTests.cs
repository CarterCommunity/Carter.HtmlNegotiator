using System;
using Carter.HtmlNegotiator.Tests.Stubs;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class ViewLocatorTests
    {
        [Fact]
        public void Should_Return_Null_When_View_Name_Is_Null()
        {
            var subject = new ViewLocator(new StubFileSystem(Array.Empty<string>()));

            var result  = subject.GetViewLocation(new DefaultHttpContext(), null, string.Empty, null);
            
            result.ShouldBeNull();
        }
        
        [Fact]
        public void Should_Return_Null_When_View_Name_Is_Empty()
        {
            var subject = new ViewLocator(new StubFileSystem(Array.Empty<string>()));

            var result  = subject.GetViewLocation(new DefaultHttpContext(), null, string.Empty, string.Empty);
            
            result.ShouldBeNull();
        }

        [Fact]
        public void Should_Use_View_Location_Conventions_To_Find_Views()
        {
            var fileSystem = new StubFileSystem(new []{ "Views/Index.hbs" });
            var subject = new ViewLocator(fileSystem);

            var result  = subject.GetViewLocation(new DefaultHttpContext(), new []{ "Views/{view}" }, string.Empty, "Index.hbs");
            
            result.ShouldNotBeNull();
            result.ShouldBe("Views/Index.hbs");
        }

        [Fact]
        public void Should_Be_Able_To_Resolve_Default_Resource_Name()
        {
            var fileSystem = new StubFileSystem(new []{ "Views/Home/Index.hbs" });
            var subject = new ViewLocator(fileSystem);

            var result  = subject.GetViewLocation(new DefaultHttpContext(), new []{ "Views/{resource}/{view}" }, "Home", "Index.hbs");
            
            result.ShouldNotBeNull();
            result.ShouldBe("Views/Home/Index.hbs");
        }
        
        [Theory]
        [InlineData("/Products", "Index.hbs", "Views/Products/Index.hbs")]
        [InlineData("/Orders/Checkout", "Checkout.hbs", "Views/Orders/Checkout.hbs")]
        public void Should_Be_Able_To_Resolve_Resource_Name_From_Request_Path(string requestPath, string viewName, string expected)
        {
            var fileSystem = new StubFileSystem(new []
            {
                "Views/Products/Index.hbs",
                "Views/Orders/Checkout.hbs"
            });
            var subject = new ViewLocator(fileSystem);

            var context = new DefaultHttpContext();
            context.Request.Path = requestPath;
            var result  = subject.GetViewLocation(context, new []{ "Views/{resource}/{view}" }, "Home", viewName);
            
            result.ShouldNotBeNull();
            result.ShouldBe(expected);
        }
        
        [Fact]
        public void Should_Be_Throw_An_AmbiguousViewsException_When_Multiple_View_Are_Located()
        {
            var fileSystem = new StubFileSystem(new []
            {
                "Features/Products/Index.hbs",
                "Views/Products/Index.hbs"
            });
            var subject = new ViewLocator(fileSystem);

            var context = new DefaultHttpContext();
            context.Request.Path = "/Products";
            
            var locationConventions = new []
            {
                "Features/{resource}/{view}",
                "Views/{resource}/{view}"
            };
            
            var ex = Should.Throw<AmbiguousViewsException>(() => subject.GetViewLocation(context, locationConventions, "Home", "Index.hbs"));
            
            ex.Message.ShouldContain("Views (2)");
        }
        
        [Fact]
        public void Should_Be_Throw_An_ViewNotFoundException_When_No_Views_Are_Located()
        {
            var fileSystem = new StubFileSystem(new string[] {});
            var subject = new ViewLocator(fileSystem);

            var context = new DefaultHttpContext();
            context.Request.Path = "/Products";
            
            var locationConventions = new []
            {
                "Views/{resource}/{view}"
            };
            
            var ex = Should.Throw<ViewNotFoundException>(() => subject.GetViewLocation(context, locationConventions, "Home", "Index.hbs"));
            
            ex.Message.ShouldContain("The view 'Index.hbs' was not found");
        }
    }
}