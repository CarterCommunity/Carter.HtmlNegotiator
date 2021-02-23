using System.Collections.Generic;
using Carter.HtmlNegotiator.Tests.Stubs;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class HandlebarsViewEngineTests
    {
        [Fact]
        public void Should_Return_Compiled_HTML()
        {
            var handlebars = Handlebars.Create();
            var viewResolver = new StubViewResolver(new Dictionary<string, string>
            {
                ["Index.hbs"] = "<h3>Hello from {{Name}}!</h3>"
            });
            
            var configuration = new HtmlNegotiatorConfiguration(new []{ "Views/{Resource}/{View}" });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/";

            var subject = new HandlebarsViewEngine(handlebars, viewResolver, new ViewNameResolver(configuration), configuration);

            var result = subject.GetView(httpContext, new { Name = "Carter" });

            Assert.Equal("<h3>Hello from Carter!</h3>", result);
        }
        
        [Fact]
        public void Should_Return_Compiled_HTML_Using_A_Layout()
        {
            var handlebars = Handlebars.Create();
            var viewLocator = new StubViewResolver(new Dictionary<string, string>
            {
                ["Index.hbs"] = "{{#*inline \"content\"}}<h3>Hello from Carter!</h3>{{/inline}}",
                ["Layout.hbs"] = "<div>{{> content}}</div>"
            });
            
            var configuration = new HtmlNegotiatorConfiguration(new []
            {
                "Views/{Resource}/{View}",
                "Views/Shared/{View}"
            });
            
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/";
            
            var subject = new HandlebarsViewEngine(handlebars, viewLocator, new ViewNameResolver(configuration), configuration);
        
            var result = subject.GetView(httpContext, new { Name = "Carter" });
        
            Assert.Equal("<div><h3>Hello from Carter!</h3></div>", result);
        }
        
        [Fact]
        public void Should_Return_Compiled_HTML_Using_A_Layout_With_A_Partial()
        {
            var handlebars = Handlebars.Create(new HandlebarsConfiguration
            {
                PartialTemplateResolver = new StubPartialResolver(new Dictionary<string, string>
                {
                    ["Footer"] = "<div>I'm A Footer</div>"
                })
            });
            
            var viewLocator = new StubViewResolver(new Dictionary<string, string>
            {
                ["Index.hbs"] = "{{#*inline \"content\"}}<h3>Hello from Carter!</h3>{{/inline}}",
                ["Layout.hbs"] = "<div>{{> content}}</div>{{> Footer}}"
            });
            
            var configuration = new HtmlNegotiatorConfiguration(new []
            {
                "Views/{Resource}/{View}",
                "Views/Shared/{View}"
            });
            
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/";
            
            var subject = new HandlebarsViewEngine(handlebars, viewLocator, new ViewNameResolver(configuration), configuration);
        
            var result = subject.GetView(httpContext, new { Name = "Carter" });
        
            Assert.Equal("<div><h3>Hello from Carter!</h3></div><div>I'm A Footer</div>", result);
        }
    }
}