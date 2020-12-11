using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
	internal static class ValidationErrorExtensions
	{
		public static IServiceCollection AddCustomValidationError(this IServiceCollection services) =>
			 services.Configure<ApiBehaviorOptions>(options =>
			 {
				 options.InvalidModelStateResponseFactory = ctx => new ValidationErrorResult(ctx.ModelState);
			 });
	}
}

namespace Microsoft.AspNetCore.Mvc
{
	internal static class ValidationErrorExtensions
	{
		public static ValidationErrorResult ValidationError(IEnumerable<Exception> exceptions) =>
			new ValidationErrorResult(exceptions);

		public static ValidationErrorResult ValidationError(params Exception[] exceptions) =>
		  new ValidationErrorResult(exceptions);

		public static ValidationErrorResult ValidationError(this ControllerBase controller, ModelStateDictionary modelState) =>
			new ValidationErrorResult(modelState);

		public static ValidationErrorResult ValidationError(this ControllerBase controller, IEnumerable<Exception> exceptions) =>
			new ValidationErrorResult(exceptions);

		public static ValidationErrorResult ValidationError(this ControllerBase controller, params Exception[] exceptions) =>
			 new ValidationErrorResult(exceptions);
	}
}
