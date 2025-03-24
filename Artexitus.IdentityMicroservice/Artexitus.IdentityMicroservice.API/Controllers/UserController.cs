using Artexitus.IdentityMicroservice.Contracts.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.API.Controllers
{
    [Route("api/identity/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private static readonly CookieOptions defaultCookieOptions = new()
        {
            SameSite = SameSiteMode.Lax,
            HttpOnly = true,
            Secure = true
        };

        public UserController(ISender sender) {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand request)
        {
            await _sender.Send(request);

            return Ok();
        }

        [HttpPost("activate")]
        public async Task<IActionResult> ActivateUserAccount([FromQuery] ActivateAccountCommand request)
        {
            await _sender.Send(request);

            return Ok();
        }
    }
}
