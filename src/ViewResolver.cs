namespace Carter.HtmlNegotiator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ViewResolver : IViewResolver
    {
        private readonly IViewLocator viewLocator;
        private readonly IEnumerable<Func<ViewLocationContext, string>> conventions;

        public ViewResolver(IViewLocator viewLocator, HtmlNegotiatorConfiguration configuration)
        {
            this.viewLocator = viewLocator;
            this.conventions = configuration.ViewLocationConventions;
        }
        public ViewTemplate ResolveView(ViewLocationContext viewLocationContext)
        {
            foreach (var convention in this.conventions)
            {
                var viewLocation = convention.Invoke(viewLocationContext);
                
                if (string.IsNullOrEmpty(viewLocation))
                {
                    continue;
                }
                
                var viewTemplate = this.viewLocator.LocateView(Path.Combine(viewLocationContext.RootPath, viewLocation));
                if (viewTemplate != null)
                {
                    return viewTemplate;
                }
            }

            return null;
        }
    }
}
