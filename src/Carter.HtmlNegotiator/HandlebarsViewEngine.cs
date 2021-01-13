using System.IO;
using HandlebarsDotNet;

namespace Carter.HtmlNegotiator
{
    public class HandlebarsViewEngine : IViewEngine
    {
        public string Compile(string viewLocation, object model)
        {
            var source = File.ReadAllText(viewLocation);
            var template = Handlebars.Compile(source);
            return template(model);
        }

        public string Extension => "hbs";
    }
}