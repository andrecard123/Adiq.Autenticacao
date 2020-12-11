using Microsoft.IdentityModel.Tokens;

namespace Adiq.Backend.Security
{
    public interface ISigningConfigurations
    {
        SecurityKey Key { get; }
        SigningCredentials SigningCredentials { get; }
    }
}
