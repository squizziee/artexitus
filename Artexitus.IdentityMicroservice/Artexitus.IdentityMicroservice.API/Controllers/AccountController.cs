using Artexitus.IdentityMicroservice.API.Attributes;
using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Users;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        private static readonly CookieOptions defaultCookieOptions = new()
        {
            SameSite = SameSiteMode.Lax,
            HttpOnly = true,
            Secure = true,
        };
        private static readonly CookieOptions logoutCookieOptions = new()
        {
            SameSite = SameSiteMode.Lax,
            HttpOnly = true,
            Secure = true,
            MaxAge = TimeSpan.FromSeconds(0)
        };

        public AccountController(ISender sender) {
            _sender = sender;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{Id:guid}")]
        [Authorize(Policy = "DefaultPolicy")]
        public async Task<IActionResult> GetUserById([FromRoute] GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{Username}")]
        [Authorize(Policy = "DefaultPolicy")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] GetUserByUsernameQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Login([FromForm] LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var tokens = await _sender.Send(request, cancellationToken);

            Response.Cookies.Append("accessToken", tokens.AccessToken, defaultCookieOptions);
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, defaultCookieOptions);

            return Ok();
        }

        [HttpPost("log-out")]
        [Authorize(Policy = "DefaultPolicy")]
        [ExtractIdFromCookie]
        public async Task<IActionResult> Logout(LogoutUserCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            Response.Cookies.Append("accessToken", string.Empty, logoutCookieOptions);
            Response.Cookies.Append("refreshToken", string.Empty, logoutCookieOptions);

            return Ok();
        }

        [HttpPost("new-tokens")]
        [AuthorizeWithRefreshToken]
        [ExtractRefreshTokenFromCookie]
        public async Task<IActionResult> RefreshTokens(RefreshTokensCommand request,
            CancellationToken cancellationToken)
        {
            var tokens = await _sender.Send(request, cancellationToken);

            Response.Cookies.Append("accessToken", tokens.AccessToken, defaultCookieOptions);
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, defaultCookieOptions);

            return Ok();
        }

        [HttpPost("me")]
        [Authorize(Policy = "DefaultPolicy")]
        [ExtractIdFromCookie]
        public async Task<IActionResult> Me(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _sender.Send(request, cancellationToken);

            return Ok(user);
        }

        [HttpGet("activation")]
        [AuthorizeWithActivationToken]
        public async Task<IActionResult> ActivateUserAccount([FromQuery] ActivateAccountCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpPatch("deactivation/{Id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeactivateAccount([FromRoute] DeactivateAccountCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "Reserved")]
        public async Task<IActionResult> DeleteUser([FromForm] DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> RequestPasswordChange([FromForm] RequestPasswordChangeCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpPatch("password-reset")]
        public async Task<IActionResult> ChabgePassword([FromForm] ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            Response.Cookies.Append("accessToken", string.Empty, logoutCookieOptions);
            Response.Cookies.Append("refreshToken", string.Empty, logoutCookieOptions);

            return Ok();
        }
    }
}
