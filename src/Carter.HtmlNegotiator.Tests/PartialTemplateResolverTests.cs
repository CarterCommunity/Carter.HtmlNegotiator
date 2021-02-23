using System.Collections.Generic;
using Carter.HtmlNegotiator.Tests.Stubs;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class PartialTemplateResolverTests
    {
        [Fact]
        public void ShouldBeAbleToResolveSharedPartials()
        {
            var stubHandlebarsEnvironment = new StubHandlebarsEnvironment();
            var locationConventions = new []
            {
                "Features/Shared/{View}"
            };
            
            var context = new DefaultHttpContext();
            context.Request.Path = "/";
            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = context
            };

            var stubFileSystem = new StubFileSystem(new Dictionary<string, string>{ ["Features/Shared/Footer.hbs"] = "<div>A Footer</div>" });
            var subject = new PartialTemplateResolver(new HtmlNegotiatorConfiguration(locationConventions),new StubWebHostEnvironment(), stubFileSystem);

            var result = subject.TryRegisterPartial(stubHandlebarsEnvironment, "Footer", null);
            
            result.ShouldBeTrue();
            stubHandlebarsEnvironment.Templates.ContainsKey("Footer").ShouldBeTrue();
            stubHandlebarsEnvironment.Templates["Footer"].ShouldBe("<div>A Footer</div>");
        }
    }
}