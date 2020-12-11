using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace Adiq.Presentation.Presentation.Api.Configurations
{
    internal static class MvcConfig
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services
                 .AddCors()
                .AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    opt.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
                    opt.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
                    opt.UseCamelCasing(true);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }

        public static IApplicationBuilder UseCustomMvc(this IApplicationBuilder app)
        {
            app
                .UseCustomStatusCode()
                .UseCors(policy => policy
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin()
                            .SetIsOriginAllowed(origin => true))
                .UseMvc();

            return app;
        }
    }
}
