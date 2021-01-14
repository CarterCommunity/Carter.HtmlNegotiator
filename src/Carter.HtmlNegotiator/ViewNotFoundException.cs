using System;
using System.Collections.Generic;

namespace Carter.HtmlNegotiator
{
    public class ViewNotFoundException : Exception
    {
        private static readonly string message = "The view '{0}' was not found. The following locations were searched:" +
                                                 Environment.NewLine +
                                                 "{1}";

        public ViewNotFoundException(string viewName, IEnumerable<string> locations) 
            : base(string.Format(message, viewName, string.Join(Environment.NewLine, locations)))
        {
        }
    }
}