using FastEndpoints;
using BookingSystem.Booking.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Booking.Api.Features.Events.Delete;

public record DeleteEventRequest(Guid Id);

public class DeleteEventEndpoint(BookingDbContext db) : Endpoint<DeleteEventRequest>
{
    public override void Configure()
    {
        Delete("/api/events/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteEventRequest req, CancellationToken ct)
    {
        var affected = await db.Events
            .Where(e => e.Id == req.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true), ct);

        if (affected == 0)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}