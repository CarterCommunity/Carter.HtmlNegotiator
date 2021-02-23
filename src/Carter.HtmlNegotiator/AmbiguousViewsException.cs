using System;
using System.Collections.Generic;
using System.Linq;

namespace Carter.HtmlNegotiator
{
    public class AmbiguousViewsException : Exception
    {
        private static readonly string message = "Multiple views found. Views ({0})," +
                                                 Environment.NewLine + "{1}";

        public AmbiguousViewsException(IEnumerable<string> views) 
            : base(string.Format(message, views.Count(), string.Join(Environment.NewLine, views)))
        {
        }
    }
}