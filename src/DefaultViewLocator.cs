namespace Carter.HtmlNegotiator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DefaultViewLocator : IViewLocator
    {
        private readonly IEnumerable<IViewEngine> viewEngines;
        private readonly IDirectoryService directoryService;
        
        private static readonly IDictionary<string, ViewTemplate> ViewCache = new Dictionary<string, ViewTemplate>();

        public DefaultViewLocator(IEnumerable<IViewEngine> viewEngines, IDirectoryService directoryService)
        {
            this.viewEngines = viewEngines;
            this.directoryService = directoryService;
        }

        public ViewTemplate LocateView(string viewLocation)
        {
            return GetViewFromCache(viewLocation) 
                   ?? this.LocateAndCacheView(viewLocation);
        }

        private static ViewTemplate GetViewFromCache(string viewLocation)
        {
            return ViewCache.ContainsKey(viewLocation)
                ? ViewCache[viewLocation]
                : null;
        }

        private ViewTemplate LocateAndCacheView(string viewLocation)
        {
            var supportedExtensions = this.viewEngines.SelectMany(e => e.SupportedExtensions).Distinct().ToList();

            var viewName = Path.GetFileNameWithoutExtension(viewLocation);
            var path = viewLocation.Substring(0, viewLocation.LastIndexOf(viewName, StringComparison.Ordinal));

            IList<ViewTemplate> viewTemplates = null;
            try
            {
                viewTemplates = this.directoryService.GetViews(path, viewName, supportedExtensions).ToList();
            }
            catch (DirectoryNotFoundException)
            {
            }
            
            if (viewTemplates?.Count == 1)
            {
                var viewTemplate = viewTemplates.Single();
                ViewCache.Add(viewLocation, viewTemplate);
                return viewTemplate;
            }

            if (viewTemplates?.Count > 1)
            {
                throw new AmbiguousViewsException(GetAmbiguousViewsExceptionMessage(viewTemplates));
            }

            return null;
        }

        private static string GetAmbiguousViewsExceptionMessage(IEnumerable<ViewTemplate> viewTemplates)
        {
            return
                $"Multiple views found.{Environment.NewLine}Views:{Environment.NewLine}{string.Join(Environment.NewLine, viewTemplates.Select(GethFullPath))}";
        }

        private static string GethFullPath(ViewTemplate template)
        {
            return string.Concat(template.Location, "/", template.Name, ".", template.Extension);
        }
    }
}
