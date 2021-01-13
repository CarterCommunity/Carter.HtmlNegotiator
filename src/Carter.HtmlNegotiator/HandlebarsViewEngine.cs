using HandlebarsDotNet;

namespace Carter.HtmlNegotiator
{
    public class HandlebarsViewEngine : IViewEngine
    {
        public string Compile(string source, object model)
        {
            var template = Handlebars.Compile(source);
            return template(model);
        }

        public string Extension => "hbs";
    }
}