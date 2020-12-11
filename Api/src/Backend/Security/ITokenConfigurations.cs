using System;
namespace Adiq.Backend.Security
{
    public interface ITokenConfigurations
    {
        string Audience { get; set; }
        string Issuer { get; set; }
        TimeSpan ExpiresOn { get; set; }
    }
}
