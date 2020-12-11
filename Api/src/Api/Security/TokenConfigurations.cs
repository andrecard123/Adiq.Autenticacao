using System;
using Adiq.Backend.Security;

namespace Adiq.Presentation.Api.Security
{
    public class TokenConfigurations : ITokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public TimeSpan ExpiresOn { get; set; }
    }
}
