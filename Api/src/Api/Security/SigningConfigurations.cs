using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Adiq.Backend.Security;

namespace Adiq.Presentation.Api.Security
{
    public class SigningConfigurations : ISigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("TokenConfigurations:SecretKey");
            var symmetricKey = System.Text.Encoding.UTF8.GetBytes(secretKey);

            Key = new SymmetricSecurityKey(symmetricKey);
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}