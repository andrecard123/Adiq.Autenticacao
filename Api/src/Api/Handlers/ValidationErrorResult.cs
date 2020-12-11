using Adiq.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adiq.Common.Models.Enums;
using Adiq.Common.Exceptions;

namespace Microsoft.AspNetCore.Mvc
{
	public class ValidationErrorResult : ObjectResult
	{
		public new int? StatusCode => StatusCodes.Status400BadRequest;

		public ValidationErrorResult(ModelStateDictionary modelState)
			: base(modelState)
		{
		}

		public ValidationErrorResult(IEnumerable<Exception> exceptions)
			: base(exceptions)
		{
		}

		public ValidationErrorResult(params Exception[] exceptions)
			: base(exceptions)
		{
		}

		public ValidationErrorResult(IEnumerable<Error> errors)
			: base(errors)
		{
		}

		public override Task ExecuteResultAsync(ActionContext context)
		{
			context.HttpContext.Response.ContentType = "application/json";
			context.HttpContext.Response.StatusCode = StatusCode.GetValueOrDefault();

			if (Value is ModelStateDictionary modelState)
				Value = ConvertModelStateToErrors(modelState);
			else if (Value is IEnumerable<Exception> exceptions)
				Value = ConvertExceptionsToErrors(exceptions);
			else if (!(Value is IEnumerable<Error>))
				Value = Array.Empty<Error>();

			return base.ExecuteResultAsync(context);
		}

		public static IEnumerable<Error> ConvertModelStateToErrors(ModelStateDictionary modelState)
			=> modelState.SelectMany(entry =>
				   entry.Value.Errors.Select(erro =>
					   erro.Exception == default
					   ? new Error
					   {
						   Code = $"VALIDATION:{entry.Key}",
						   Message = erro.ErrorMessage,
						   Severity = Severity.Warning
					   }
					   : ConvertExceptionToError(erro.Exception)
				   )
			);

		public static IEnumerable<Error> ConvertExceptionsToErrors(IEnumerable<Exception> exceptions)
			=> exceptions.Select(e => ConvertExceptionToError(e));

		public static Error ConvertExceptionToError(Exception ex)
		{			
			if (ex is ValidationException validationException)
				return new Error { Code = $"VALIDATION:{validationException.ErrorCode}", Message = validationException.Message, Severity = Severity.Warning };
			else if (ex is ErrorException errorException)
				return new Error { Code = $"ERROR:{errorException.ErrorCode}", Message = errorException.Message, Severity = errorException.Severity };

			return new Error { Code = ex.GetType().Name, Message = ex.Message, Severity = Severity.Error };
		}
	}
}
