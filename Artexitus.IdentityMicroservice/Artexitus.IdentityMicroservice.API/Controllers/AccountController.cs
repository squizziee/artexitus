using Artexitus.IdentityMicroservice.API.Middleware;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        private static readonly CookieOptions defaultCookieOptions = new()
        {
            SameSite = SameSiteMode.Lax,
            HttpOnly = true,
            Secure = true
        };

        public AccountController(ISender sender) {
            _sender = sender;
        }


        [HttpGet]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery request)
        {
            var result = await _sender.Send(request);

            return Ok(result);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand request)
        {
            await _sender.Send(request);

            return Ok();
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> LoginUser([FromForm] LoginUserCommand request)
        {
            var tokens = await _sender.Send(request);

            Response.Cookies.Append("accessToken", tokens.AccessToken, defaultCookieOptions);
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, defaultCookieOptions);

            return Ok();
        }

        [HttpPost("new-tokens")]
        [ExtractRefreshTokenFromCookie]
        public async Task<IActionResult> RefreshTokens(RefreshTokensCommand request)
        {
            var tokens = await _sender.Send(request);

            Response.Cookies.Append("accessToken", tokens.AccessToken, defaultCookieOptions);
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, defaultCookieOptions);

            return Ok();
        }

        [HttpPost("activation")]
        public async Task<IActionResult> ActivateUserAccount([FromQuery] ActivateAccountCommand request)
        {
            await _sender.Send(request);

            return Ok();
        }
    }
}
