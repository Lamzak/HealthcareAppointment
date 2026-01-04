using HealthcareAppointment.Application.Features.Authentication.Login;
using HealthcareAppointment.Application.Features.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Ok(new { token = result.Value }) : Unauthorized(result.Error);
        }
    }
}
