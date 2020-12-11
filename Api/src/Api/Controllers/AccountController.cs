using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Adiq.Common.Models;
using Adiq.Backend.Services.Account;

namespace Adiq.Presentation.Api.Controllers
{
    [OpenApiTag("Account", AddToDocument = true, Description = "")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.BadRequest)]  
    [ProducesResponseType(typeof(Error[]), (int)HttpStatusCode.InternalServerError)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [OpenApiOperation("Authenticate user credential and return token.")]
        [Route("Authenticate"), HttpPost]     
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Return>> Authenticate([FromBody] Credential credential)
        {
            var authenticationResult = await _accountService.AuthenticateCredential(credential);

            if (authenticationResult.IsFailure)
                return this.ValidationError(authenticationResult.Failure);

            return Ok(authenticationResult.Success);
        }
    }
}
