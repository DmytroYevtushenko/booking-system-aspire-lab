using FastEndpoints;
using System.Runtime.InteropServices;

namespace BookingSystem.Booking.Api.Features.Status;

public class GetStatusEndpoint : EndpointWithoutRequest<StatusResponse>
{
    public override void Configure()
    {
        Get("/api/status");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        Response = new StatusResponse("Booking API is online", RuntimeInformation.FrameworkDescription);
        return Task.CompletedTask;
    }
}

public record StatusResponse(string Message, string Runtime);