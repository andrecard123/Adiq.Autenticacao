using Adiq.Common.Models;
using System;
using System.Threading.Tasks;

namespace Adiq.Backend.Services.Account
{
    public interface IAccountService
    {
        Task<Try<UserToken>> AuthenticateCredential(Credential credential);
    }
}
