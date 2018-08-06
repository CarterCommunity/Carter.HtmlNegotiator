namespace Carter.HtmlNegotiator
{
    using System;
    using Carter;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtentions
    {   
        public static void AddHtmlNegotiator(this IServiceCollection services, Action<HtmlNegotiatorConfigurator> configuration = default)
        {
            AddDefaultServices(services, new HtmlNegotiatorConfiguration(configuration));
        }

        private static void AddDefaultServices(IServiceCollection services, HtmlNegotiatorConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<IDirectoryService, DirectoryService>();
            services.AddScoped<IViewLocator, DefaultViewLocator>();
            services.AddScoped<IViewEngine, HandlebarsViewEngine>();
            services.AddScoped<IViewResolver, ViewResolver>();
            services.AddScoped<IViewRenderer, ViewRenderer>();
            services.AddScoped<IResponseNegotiator, HtmlNegotiator>();
        }
    }
}