using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class ViewNameResolverTests
    {
        [Fact]
        public void Should_Return_Default_View_Name_With_Extension()
        {
            var subject = new ViewNameResolver();

            var result = subject.Resolve(new DefaultHttpContext(), "Index", "hbs");
            
            result.ShouldBe("Index.hbs");
        }
        
        [Fact]
        public void Should_Return_View_Name_From_HTTP_Context_With_Extension()
        {
            var subject = new ViewNameResolver();

            var context = new DefaultHttpContext();
            context.Items.Add(Constants.ViewNameKey, "my-view");
            
            var result = subject.Resolve(context, "Index", "hbs");
            
            result.ShouldBe("my-view.hbs");
        }
        
        [Fact]
        public void Should_Not_Append_Extension_When_Included_In_HttpContext()
        {
            var subject = new ViewNameResolver();

            var context = new DefaultHttpContext();
            context.Items.Add(Constants.ViewNameKey, "my-view.hbs");
            
            var result = subject.Resolve(context, "Index", "hbs");
            
            result.ShouldBe("my-view.hbs");
        }
        
        [Theory]
        [InlineData(null, "Index.hbs")]
        [InlineData("", "Index.hbs")]
        [InlineData("/", "Index.hbs")]
        [InlineData("/about", "about.hbs")]
        [InlineData("/orders/checkout", "checkout.hbs")]
        public void Should_Return_View_Name_From_Request_Path_With_Extension(string path, string expected)
        {
            var subject = new ViewNameResolver();

            var context = new DefaultHttpContext();
            context.Request.Path = path;
            
            var result = subject.Resolve(context, "Index", "hbs");
            
            result.ShouldBe(expected);
        }
    }
}