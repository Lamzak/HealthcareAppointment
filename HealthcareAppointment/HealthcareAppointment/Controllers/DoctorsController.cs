using HealthcareAppointment.Application.Features.Doctors.GetAll;
using HealthcareAppointment.Application.Features.Doctors.GetRecommended;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetDoctorsQuery());
            return Ok(result.Value);
        }

        [HttpPost("recommend")]
        public async Task<IActionResult> Recommend([FromBody] GetRecommendedDoctorsQuery query)
        {
            var result = await mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
