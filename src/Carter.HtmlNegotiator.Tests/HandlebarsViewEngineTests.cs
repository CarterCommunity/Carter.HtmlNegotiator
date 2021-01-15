using HandlebarsDotNet;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class HandlebarsViewEngineTests
    {
        [Fact]
        public void BasicPartial()
        {
            var source = "{{#> layouts/base title=\"Welcome Page\" }}{{#*inline \"content\"}}<h3>Hello from Carter!</h3>{{/inline}}{{/layouts/base}}";

            var handlebars = Handlebars.Create(new HandlebarsConfiguration
            {
                PartialTemplateResolver = new CustomPartialResolver()
            });


            var template = handlebars.Compile(source);

            var data = new {
                name = "Marc"
            };
            
            var result = template(data);
            Assert.Equal("Hello, Marc!", result);
        }
    }
    
    public class CustomPartialResolver : IPartialTemplateResolver
    {
        public bool TryRegisterPartial(IHandlebars env, string partialName, string templatePath)
        {
            if (partialName == "person")
            {
                env.RegisterTemplate("person", "{{name}}");
                return true;
            }

            return false;
        }
    }
}