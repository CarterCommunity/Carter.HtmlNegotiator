namespace HtmlNegotiator.Tests
{
    using System.Collections.Generic;
    using Carter.HtmlNegotiator;
    using FakeItEasy;
    using Xunit;

    public class ViewLocatorTests
    {
        private readonly IDirectoryService directoryService;
        private readonly DefaultViewLocator defaultViewLocator;

        public ViewLocatorTests()
        {
            var stubViewEngine = new StubViewEngine();
            directoryService = A.Fake<IDirectoryService>();
            defaultViewLocator = new DefaultViewLocator( new []{ stubViewEngine }, directoryService);
        }
        
        [Fact(Skip = "Causes The Other Two Tests To Fail")]
        public void Should_Return_A_ViewTemplate_When_A_View_Is_Located()
        {
            // ARRANGE
            A.CallTo(() => directoryService.GetViews(A<string>.Ignored, A<string>.Ignored, A<List<string>>.Ignored))
                .Returns(new List<ViewTemplate> { new ViewTemplate() });
            
            // ACT
            var viewTemplate = defaultViewLocator.LocateView("/a/path/aViewName.hbs");

            // ASSERT
            Assert.NotNull(viewTemplate);
        }
        
        [Fact]
        public void Should_Return_Null_When_No_View_Is_Located()
        {
            // ARRANGE
            A.CallTo(() => directoryService.GetViews(A<string>.Ignored, A<string>.Ignored, A<List<string>>.Ignored))
                .Returns(new List<ViewTemplate>());
            
            // ACT
            var viewTemplate = defaultViewLocator.LocateView("/a/path/aViewName.hbs");

            // ASSERT
            Assert.Null(viewTemplate);
        }
        
        [Fact]
        public void Should_Throw_An_AmbiguousViewException_When_More_Than_One_View_Is_Located()
        {
            // ARRANGE
            A.CallTo(() => directoryService.GetViews(A<string>.Ignored, A<string>.Ignored, A<List<string>>.Ignored))
                .Returns(new List<ViewTemplate> {new ViewTemplate(), new ViewTemplate()});
            
            // ACT
            // ASSERT
            Assert.Throws<AmbiguousViewsException>(() => defaultViewLocator.LocateView("/a/path/aViewName.hbs"));
        }
    }
}