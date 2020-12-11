using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Adiq.Common.Models;
using Adiq.Common.Exceptions;
using Adiq.Common.Models.Enums;

namespace Microsoft.AspNetCore.Builder
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ErrorHandlerMiddleware(
			RequestDelegate next
			, ILogger<ErrorHandlerMiddleware> logger
		)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro interno não tratado");

				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var error = (exception is ErrorException errorException)
				? errorException
				: new ErrorException("INTERNAL_ERROR", exception.Message, exception, Severity.Error);

			var result = JsonSerialize(new[]
			{
				new Error
                {
					Code = $"ERROR:{error.ErrorCode}",
					Message = error.Message,
					Severity = error.Severity
				}
			});

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			return context.Response.WriteAsync(result);
		}

		private static string JsonSerialize(object @object) =>
			Newtonsoft.Json.JsonConvert.SerializeObject(
				@object
				, new Newtonsoft.Json.JsonSerializerSettings
				{
					ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
					DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Unspecified,
					Converters = new[] { new Newtonsoft.Json.Converters.StringEnumConverter() }
				}
			);
	}
}
