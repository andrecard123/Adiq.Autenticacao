using Adiq.Common.Models.Enums;
using System;
using System.Runtime.Serialization;

namespace Adiq.Common.Exceptions
{
	[Serializable]
	public abstract class ApiException : Exception
	{

		public Severity Severity { get; set; }

		protected ApiException()
			: base()
		{
		}

		protected ApiException(string message)
			: base(message)
		{
		}

		protected ApiException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ApiException(string message, Exception innerException, Severity severity)
			: base(message, innerException)
		{
			Severity = severity;
		}

		protected ApiException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
