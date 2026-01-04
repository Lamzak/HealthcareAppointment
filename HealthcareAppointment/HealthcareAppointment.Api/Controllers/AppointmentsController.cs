using HealthcareAppointment.Application.Features.Appointments.Add;
using HealthcareAppointment.Application.Features.Appointments.GetAll;
using HealthcareAppointment.Application.Features.Appointments.Cancel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointment.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMyAppointments()
        {
            var result = await mediator.Send(new GetMyAppointmentsQuery());
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(AddAppointmentCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(Guid id)
        {
            var result = await mediator.Send(new CancelAppointmentCommand(id));
            return result.IsSuccess ? Ok("Appointment cancelled successfully.") : BadRequest(result.Error);
        }
    }
}
