using Adiq.Backend.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Adiq.Presentation.Api.Security;
using Microsoft.Extensions.Options;
using Adiq.Backend.Security;

namespace Adiq.Presentation.Presentation.Api.Configurations
{
    internal static class ApplicationConfig
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddBackend(configuration)
                .AddConfigurations(configuration);

        private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            services.AddSingleton<ITokenConfigurations>(tokenConfigurations);

            var signingConfigurations = new SigningConfigurations(configuration);
            services.AddSingleton<ISigningConfigurations>(signingConfigurations);

            return services;
        }
    }
}
