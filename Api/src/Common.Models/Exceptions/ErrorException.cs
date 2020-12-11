using Adiq.Common.Models.Enums;
using System;
using System.Runtime.Serialization;

namespace Adiq.Common.Exceptions
{
    [Serializable]
    public class ErrorException : ApiException
    {
        public string ErrorCode { get; }

        public ErrorException(string errorCode)
            : base()
        {
            ErrorCode = errorCode;
            SetDefaultSeverity();
        }

        public ErrorException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
            SetDefaultSeverity();
        }

        public ErrorException(string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            SetDefaultSeverity();
        }

        private void SetDefaultSeverity()
        {
            Severity = Severity.Error;
        }

        public ErrorException(string errorCode, string message, Exception innerException, Severity severity)
        : base(message, innerException)
        {
            ErrorCode = errorCode;
            Severity = severity;
        }

        protected ErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
