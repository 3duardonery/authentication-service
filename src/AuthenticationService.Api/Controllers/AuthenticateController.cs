using AuthenticationService.Application.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest authenticateUserRequest)
        {
            var result = await _mediator.Send(authenticateUserRequest);

            if (!result.IsSuccess)
                return Unauthorized(result.Exception.Message);

            return Ok(result.Value);
        }
    }
}
