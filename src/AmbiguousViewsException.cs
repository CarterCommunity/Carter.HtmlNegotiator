namespace Carter.HtmlNegotiator
{
    using System;

    public class AmbiguousViewsException : Exception
    {
        public AmbiguousViewsException(string message) : base(message) {}
    }
}