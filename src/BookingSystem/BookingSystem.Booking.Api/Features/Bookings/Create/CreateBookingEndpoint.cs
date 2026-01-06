using FastEndpoints;
using BookingSystem.Booking.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Booking.Api.Features.Bookings.Create;

public class CreateBookingEndpoint(BookingDbContext db)
    : Endpoint<CreateBookingRequest, CreateBookingResponse, CreateBookingMapper>
{
    public override void Configure()
    {
        Post("/api/bookings");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookingRequest req, CancellationToken ct)
    {
        var eventExists = await db.Events.AnyAsync(e => e.Id == req.EventId, ct);
        if (!eventExists)
        {
            await Send.ErrorsAsync(404, ct);
            return;

        }

        var entity = Map.ToEntity(req);

        db.Bookings.Add(entity);
        await db.SaveChangesAsync(ct);

        var response = Map.FromEntity(entity);
        await Send.OkAsync(response, ct);
    }
    
}