using Adiq.Backend.Services.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adiq.Backend.Configurations
{
    public static class BackendConfig
    {
        public static IServiceCollection AddBackend(this IServiceCollection services, IConfiguration configuration)
           => services
               .AddBackendServices();

        private static IServiceCollection AddBackendServices(this IServiceCollection services)
           => services
               .AddScoped<IAccountService, AccountService>();
    }
}
