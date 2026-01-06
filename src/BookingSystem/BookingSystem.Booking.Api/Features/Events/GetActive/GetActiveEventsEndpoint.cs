using FastEndpoints;
using BookingSystem.Booking.Api.Features.Bookings.Create;
using BookingSystem.Booking.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Booking.Api.Features.Events.GetActive;

public record GetActiveEventsResponse(List<EventResponse> Events);
public record EventResponse(Guid Id, string Title, string Description, DateTime Date);

public class GetActiveEventsEndpoint(BookingDbContext db) : EndpointWithoutRequest<GetActiveEventsResponse>
{
    public override void Configure()
    {
        Get("/api/events/active");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var activeEvents = await db.Events
            .Where(e => e.IsActive && e.Date >= DateTime.UtcNow)
            .Select(e => new EventResponse(e.Id, e.Title, e.Description, e.Date))
            .ToListAsync(ct);

        await Send.OkAsync(new GetActiveEventsResponse(activeEvents), ct);
    }
}