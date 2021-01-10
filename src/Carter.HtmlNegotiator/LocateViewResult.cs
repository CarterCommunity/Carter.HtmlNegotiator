using System.Collections.Generic;

namespace Carter.HtmlNegotiator
{
    public class LocateViewResult
    {
        private LocateViewResult(string viewName, string view, List<string> searchedLocations)
        {
            ViewName = viewName;
            View = view;
            SearchedLocations = searchedLocations;
        }
        
        public string View { get; }

        public string ViewName { get; }

        public List<string> SearchedLocations { get; }
        
        public bool Success => !string.IsNullOrEmpty(View);

        public static LocateViewResult Found(string viewName, string view) => new LocateViewResult(viewName, view, null);
        
        public static LocateViewResult NotFound(string viewName, List<string> searchedLocations) => new LocateViewResult(viewName, null, searchedLocations);
    }
}