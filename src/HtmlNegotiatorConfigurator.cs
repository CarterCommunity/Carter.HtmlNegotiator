namespace Carter.HtmlNegotiator
{
    using System;

    public class HtmlNegotiatorConfigurator
    {
        private readonly HtmlNegotiatorConfiguration configuration;

        public HtmlNegotiatorConfigurator(HtmlNegotiatorConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public HtmlNegotiatorConfigurator ViewLocation(Func<ViewLocationContext, string> convention)
        {
            this.configuration.ViewLocationConventions.Add(convention);
            return this;
        }
    }
}