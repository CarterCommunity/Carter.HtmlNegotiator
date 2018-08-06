namespace Carter.HtmlNegotiator
{
    using System;
    using System.Collections.Generic;

    public class HtmlNegotiatorConfiguration
    {
        public HtmlNegotiatorConfiguration(Action<HtmlNegotiatorConfigurator> configuration = default)
        {
            ViewLocationConventions = new List<Func<ViewLocationContext, string>>
            {
                viewLocationContext => viewLocationContext.ViewName,
                viewLocationContext => $"views/{viewLocationContext.ViewName}",
                viewLocationContext => $"{viewLocationContext.ModuleName}/{viewLocationContext.ViewName}",
                viewLocationContext => $"views/{viewLocationContext.ModuleName}/{viewLocationContext.ViewName}"
            };
            
            if (configuration != null)
            {
                var configurator = new HtmlNegotiatorConfigurator(this);
                configuration.Invoke(configurator);
            }
        }

        public IList<Func<ViewLocationContext, string>> ViewLocationConventions { get; set; }
    }
}