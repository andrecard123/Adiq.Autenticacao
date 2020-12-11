
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace Adiq.Presentation.Presentation.Api.Configurations
{
	public static class LoggingConfig
	{
		public static IServiceCollection AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			services
				.AddLogging(logging =>
				{
					logging
						.AddConfiguration(configuration.GetSection("Logging"))
						.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true, IncludeScopes = false });

					if (typeof(NLog.LayoutRenderers.WindowsIdentityLayoutRenderer).IsVisible) // Workarround para carregar o WindowsIdentityLayout
						NLog.LogManager.LoadConfiguration("Logs.config");
				});

			return services;
		}
	}
}
