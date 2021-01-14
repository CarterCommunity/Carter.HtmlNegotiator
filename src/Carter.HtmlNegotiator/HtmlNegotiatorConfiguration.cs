using System.Collections.Generic;

namespace Carter.HtmlNegotiator
{
    public class HtmlNegotiatorConfiguration
    {
        public HtmlNegotiatorConfiguration(List<string> viewLocationConventions)
        {
            DefaultViewName = "Index";
            RootResourceName = "Home";
            ViewLocationConventions = viewLocationConventions;
        }
        
        public string DefaultViewName { get; }
        
        public string RootResourceName { get; set; }
        
        public List<string> ViewLocationConventions { get; }
        
    }
}