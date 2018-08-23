namespace Carter.HtmlNegotiator
{
    using System.Collections.Generic;
    using HandlebarsDotNet;

    public class HandlebarsViewEngine : IViewEngine
    {
        public IEnumerable<string> SupportedExtensions { get; }

        public HandlebarsViewEngine()
        {
            this.SupportedExtensions = new List<string>{ "hbs" };
        }

        public string Render(ViewTemplate viewTemplate, object model)
        {
            var template = Handlebars.Compile(viewTemplate.Source());
            return template(model);
        }
    }
}