using Adiq.Common.Models.Enums;

namespace Adiq.Common.Models
{
    public class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public Severity Severity { get; set; } = Severity.Warning;
    }
}
