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
            PartialLocations = new[] {"Shared", "Views/Shared"};
        }
        
        public string DefaultViewName { get; }
        
        public string RootResourceName { get; }
        
        public IEnumerable<string> ViewLocationConventions { get; }
        
        public IEnumerable<string> PartialLocations { get; }
        
    }
}