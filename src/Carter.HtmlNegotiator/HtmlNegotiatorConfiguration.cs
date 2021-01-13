using System.Collections.Generic;

namespace Carter.HtmlNegotiator
{
    public class HtmlNegotiatorConfiguration
    {
        public HtmlNegotiatorConfiguration(List<string> viewLocationConventions)
        {
            DefaultViewName = "Index";
            ViewLocationConventions = viewLocationConventions;
        }
        
        public string DefaultViewName { get; }
        
        public List<string> ViewLocationConventions { get; }
        
    }
}