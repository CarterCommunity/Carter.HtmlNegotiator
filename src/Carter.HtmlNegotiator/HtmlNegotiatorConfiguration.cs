using System.Collections.Generic;

namespace Carter.HtmlNegotiator
{
    public class HtmlNegotiatorConfiguration
    {
        public HtmlNegotiatorConfiguration(IEnumerable<string> viewLocationConventions)
        {
            DefaultViewName = "Index";
            RootResourceName = "Home";
            ViewLocationConventions = viewLocationConventions;
        }
        
        public string DefaultViewName { get; }
        
        public string RootResourceName { get; }
        
        public IEnumerable<string> ViewLocationConventions { get; }
        
    }
}