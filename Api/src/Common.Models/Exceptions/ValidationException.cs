using Adiq.Common.Models.Enums;
using System;
using System.Runtime.Serialization;

namespace Adiq.Common.Exceptions
{
	[Serializable]
	public class ValidationException : ApiException
	{
		public string ErrorCode { get; }

		public ValidationException(string errorCode)
			: base()
		{
			ErrorCode = errorCode;
			SetDefaultSeverity();
		}

		public ValidationException(string errorCode, string message)
			: base(message)
		{
			ErrorCode = errorCode;
			SetDefaultSeverity();
		}

		public ValidationException(string errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorCode = errorCode;
			SetDefaultSeverity();
		}

		public ValidationException(string errorCode, string message, Exception innerException, Severity severity)
		   : base(message, innerException)
		{
			ErrorCode = errorCode;
			Severity = severity;
		}

		private void SetDefaultSeverity()
		{
            Severity = Severity.Warning;
		}

		protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
