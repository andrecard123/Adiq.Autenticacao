using System;

namespace Adiq.Common.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }

        public UserToken(string token, DateTime expiresOn)
        {
            Token = token;
            ExpiresOn = expiresOn;
        }

        public static UserToken Create(string token, DateTime expiresOn)
            => new UserToken(token, expiresOn);
    }
}
