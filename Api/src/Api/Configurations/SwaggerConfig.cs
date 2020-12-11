using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;

namespace Adiq.Presentation.Presentation.Api.Configurations
{
    internal static class SwaggerConfig
    {
        private static OpenApiInfo ApiInfo
           => new OpenApiInfo
           {
               Title = "Adiq.Api",
               Description = "Adiq.Api",
               Version = "latest",
               Contact = new OpenApiContact
               {
                   Name = "André Cardoso",
                   Url = ""
               }
           };

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services
               .AddOpenApiDocument(settings =>
               {
                   settings.AllowReferencesWithProperties = true;
                   settings.DocumentName = ApiInfo.Version;
                   settings.Version = ApiInfo.Version;

                   settings.PostProcess = document =>
                   {
                       document.Info = ApiInfo;
                       document.Produces = new[] { "application/json" };
                   };
               });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app
                .UseOpenApi()
                .UseSwaggerUi3();

            return app;
        }
    }
}
