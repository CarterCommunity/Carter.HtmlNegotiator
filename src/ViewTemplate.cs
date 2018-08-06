namespace Carter.HtmlNegotiator
{
    using System;

    public class ViewTemplate
    {
        public string Name { get; set; }

        public Func<string> Source { get; set; }
        
        public string Location { get; set; }

        public string Extension { get; set; }
    }
}