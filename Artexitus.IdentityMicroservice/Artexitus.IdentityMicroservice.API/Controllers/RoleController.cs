using Artexitus.IdentityMicroservice.Contracts.Requests.Commands.Roles;
using Artexitus.IdentityMicroservice.Contracts.Requests.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artexitus.IdentityMicroservice.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ISender _sender;

        public RoleController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{Id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "Reserved")]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "Reserved")]
        public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "Reserved")]
        public async Task<IActionResult> DeleteRole([FromForm] DeleteRoleCommand request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(request, cancellationToken);

            return Ok();
        }
    }
}
