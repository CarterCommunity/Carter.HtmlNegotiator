namespace HtmlNegotiator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class DefaultViewLocator : IViewLocator
    {
        private readonly IDictionary<Type, string> mappings;

        private readonly IDictionary<Type, string> htmlMappings;

        public DefaultViewLocator(IDictionary<Type, string> mappings)
        {
            this.mappings = mappings;
            this.htmlMappings = new Dictionary<Type, string>();
        }

        public string GetView(object model, HttpContext httpContext)
        {
            string viewName = string.Empty;
            try
            {
                viewName = this.mappings[model.GetType()];
            }
            catch (Exception)
            {
                return string.Empty;
            }

            if (this.htmlMappings.ContainsKey(model.GetType()))
            {
                return this.htmlMappings[model.GetType()];
            }

            var env = (IHostingEnvironment)httpContext.RequestServices.GetService(typeof(IHostingEnvironment));

            try
            {
                var html = File.ReadAllText(Path.Combine(env.ContentRootPath, viewName));

                this.htmlMappings.Add(model.GetType(), html);

                return html;
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }
        }
    }
}
