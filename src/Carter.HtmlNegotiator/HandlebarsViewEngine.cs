using System.IO;
using System.Text;
using HandlebarsDotNet;

namespace Carter.HtmlNegotiator
{
    public class HandlebarsViewEngine : IViewEngine
    {
        public string Compile(string viewLocation, object model)
        {
            var source = File.ReadAllText(viewLocation, Encoding.UTF8);
            var template = Handlebars.Compile(source);
            return template(model);
        }

        public string Extension => "hbs";
    }
}