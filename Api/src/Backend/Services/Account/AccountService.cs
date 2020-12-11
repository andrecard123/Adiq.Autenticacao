using Adiq.Common.Exceptions;
using Adiq.Common.Models;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Adiq.Backend.Security;

namespace Adiq.Backend.Services.Account
{
    internal class AccountService : IAccountService
    {
        private readonly ITokenConfigurations _tokenConfigurations;
        private readonly ISigningConfigurations _signingConfigurations;

        private const string FixedUsername = "lucas";
        private const string FixedPassword = "adiq";

        public AccountService(
            ITokenConfigurations tokenConfigurations,
            ISigningConfigurations signingConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        public async Task<Try<UserToken>> AuthenticateCredential(Credential credential)
        {
            if (credential.Username == FixedUsername
                && credential.Password == FixedPassword)
                return await Task.FromResult(GenerateToken(credential));

            return new ValidationException("WRONG_USERNAME_AND_OR_PASSWORD", "Usuário e/ou senha inválido.");
        }

        private UserToken GenerateToken(Credential credential)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var expiresOn = DateTime.Now.Add(_tokenConfigurations.ExpiresOn);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, credential.Username) }),
                Audience = _tokenConfigurations.Audience,
                Issuer = _tokenConfigurations.Issuer,
                Expires = expiresOn,
                SigningCredentials = _signingConfigurations.SigningCredentials
            };

            var stoken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(stoken);

            return UserToken.Create(token, expiresOn);
        }
    }
}

