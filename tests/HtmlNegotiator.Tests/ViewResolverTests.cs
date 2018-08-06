namespace HtmlNegotiator.Tests
{
    using Carter.HtmlNegotiator;
    using FakeItEasy;
    using Xunit;

    public class ViewResolverTests
    {
        [Fact]
        public void Should_Resolve_A_ViewTemplate()
        {
            // ARRANGE
            var viewLocator = A.Fake<IViewLocator>();
            A.CallTo(() => viewLocator.LocateView(A<string>.Ignored)).Returns(new ViewTemplate());
            var viewResolver = new ViewResolver(viewLocator, new HtmlNegotiatorConfiguration());
            
            var viewLocationContext = new ViewLocationContext{ RootPath = "Some/path", ModuleName = "qwerty"};
            
            // ACT
            var result = viewResolver.ResolveView(viewLocationContext);
            
            // ASSERT
            Assert.NotNull(result);
        }
        
        [Fact]
        public void Should_Return_Null_When_No_ViewTemplate_Is_Found()
        {
            // ARRANGE
            var viewLocator = A.Fake<IViewLocator>();
            A.CallTo(() => viewLocator.LocateView(A<string>.Ignored)).Returns(null);
            var viewResolver = new ViewResolver(viewLocator, new HtmlNegotiatorConfiguration());
            
            var viewLocationContext = new ViewLocationContext{ RootPath = "Some/path", ModuleName = "qwerty"};
            
            // ACT
            var result = viewResolver.ResolveView(viewLocationContext);
            
            // ASSERT
            Assert.Null(result);
        }
    }
}