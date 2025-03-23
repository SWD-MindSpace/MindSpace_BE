using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment;
using MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment;
using MindSpace.Application.Features.Appointments.Commands.HandleWebhook;
using MindSpace.Application.Features.Appointments.Queries.GetAppointmentHistoryByUser;
using MindSpace.Application.Features.Appointments.Queries.GetSessionUrl;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using Stripe.Checkout;

namespace MindSpace.API.Controllers;

public class AppointmentsController(IMediator mediator) : BaseApiController
{
    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // POST /api/appointments/booking/confirm
    [HttpPost("booking/confirm")]
    public async Task<IActionResult> ConfirmBookingAppointment([FromBody] ConfirmBookingAppointmentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    // POST /api/appointments/booking/cancel
    [HttpPost("booking/cancel")]
    public async Task<IActionResult> CancelBookingAppointment([FromBody] CancelBookingAppointmentCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    // POST /api/appointments/booking/webhook/{link}
    [HttpPost("booking/webhook/{link}")]
    public async Task<IActionResult> HandleWebhook(string link)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var command = new HandleWebhookCommand
        {
            StripeEventJson = json
        };
        await mediator.Send(command);
        return Ok();
    }

    // ==============================
    // === GET
    // ==============================

    // GET /api/appointments/booking/expire-session/{sessionId}
    // API FOR TESTING: CHECKING SESSION STATUS
    [Cache(600)]
    [HttpGet("booking/expire-session/{sessionId}")]
    public async Task<IActionResult> ExpireSession([FromRoute] string sessionId)
    {
        var session = new SessionService();
        await session.ExpireAsync(sessionId);

        return Ok("Session expired");
    }

    // GET /api/appointments/booking/session-status/{sessionId}
    // API FOR TESTING: CHECKING SESSION STATUS
    [HttpGet("booking/session-status/{sessionId}")]
    public async Task<IActionResult> GetSessionStatus([FromRoute] string sessionId)
    {
        var sessionService = new SessionService();
        var session = await sessionService.GetAsync(sessionId);
        return Ok(new { Status = session.Status, PaymentStatus = session.PaymentStatus });
    }

    // GET /api/appointments/booking/session-url/{sessionId}
    [HttpGet("booking/session-url/{sessionId}")]
    public async Task<IActionResult> GetSessionUrl([FromRoute] string sessionId)
    {
        var result = await mediator.Send(new GetSessionUrlQuery() { SessionId = sessionId });
        return Ok(result);
    }

    // GET /api/appointments/booking/user
    [Cache(30000)]
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetAppointmentsHistoryByUser([FromQuery] AppointmentSpecParams specParams)
    {
        var result = await mediator.Send(new GetAppointmentHistoryByUserQuery(specParams));
        return PaginationOkResult(
            result.Data,
            result.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }
}