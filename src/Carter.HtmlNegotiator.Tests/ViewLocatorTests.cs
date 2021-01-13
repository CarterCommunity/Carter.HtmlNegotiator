using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace Carter.HtmlNegotiator.Tests
{
    public class ViewLocatorTests
    {
        [Fact] 
        public void Should_Throw_When_HttpContext_Is_Null()
        {
            var viewLocator = new ViewLocator(new HtmlNegotiatorConfiguration(new List<string>()));
            var ex = Should.Throw<ArgumentNullException>(() =>  viewLocator.GetView(null, null));
            ex.ParamName.ShouldBe("httpContext");
        }
        
        [Fact] 
        public void Should_Throw_When_ViewName_Is_Null_Or_Empty()
        {
            var viewLocator = new ViewLocator(new HtmlNegotiatorConfiguration(new List<string>()));
            var ex = Should.Throw<ArgumentNullException>(() =>  viewLocator.GetView(new DefaultHttpContext(), null));
            ex.ParamName.ShouldBe("viewName");
        }

        [Fact]
        public void Should_Return_A_View_Located_Result()
        {
            
        }
    }
}