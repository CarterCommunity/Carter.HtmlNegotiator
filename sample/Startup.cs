namespace Carter.HtmlNegotiator.Sample
{
    using Carter;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHtmlNegotiator(with => 
                with.ViewLocation(ctx => $"features/{ctx.ModuleName}/views/{ctx.ViewName}"));
            services.AddCarter();
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory logging)
        {
            logging.AddConsole();
            logging.AddDebug();
            
            app.UseCarter();
        }
    }
}
