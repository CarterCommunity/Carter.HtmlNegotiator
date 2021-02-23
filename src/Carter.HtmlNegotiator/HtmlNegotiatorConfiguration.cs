using System.Collections.Generic;

namespace Carter.HtmlNegotiator
{
    public class HtmlNegotiatorConfiguration
    {
        public HtmlNegotiatorConfiguration(IEnumerable<string> viewLocationConventions)
        {
            DefaultViewName = "Index";
            RootResourceName = "Home";
            DefaultLayoutName = "Layout";
            ViewLocationConventions = viewLocationConventions;
        }

        public string DefaultViewName { get; }
        
        public string RootResourceName { get; }
        
        public string DefaultLayoutName { get; }
        
        public IEnumerable<string> ViewLocationConventions { get; }
    }
}