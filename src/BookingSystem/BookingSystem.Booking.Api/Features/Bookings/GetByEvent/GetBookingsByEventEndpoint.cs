using FastEndpoints;
using BookingSystem.Booking.Api.Features.Bookings.Create;
using BookingSystem.Booking.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Booking.Api.Features.Bookings.GetByEvent;

public record GetBookingsByEventRequest(Guid EventId);
public record GetBookingsByEventResponse(List<BookingResponse> Bookings);
public record BookingResponse(Guid Id, string CustomerEmail, string SeatNumber, decimal Price, BookingStatus Status);

public class GetBookingsByEventEndpoint(BookingDbContext db) : Endpoint<GetBookingsByEventRequest, GetBookingsByEventResponse>
{
    public override void Configure()
    {
        Get("/api/events/{EventId}/bookings");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookingsByEventRequest req, CancellationToken ct)
    {
        var eventExists = await db.Events.AnyAsync(e => e.Id == req.EventId, ct);
        if (!eventExists)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var bookings = await db.Bookings
            .Where(b => b.EventId == req.EventId)
            .Select(b => new BookingResponse(b.Id, b.CustomerEmail, b.SeatNumber, b.Price, b.Status))
            .ToListAsync(ct);

        await Send.OkAsync(new GetBookingsByEventResponse(bookings), ct);
    }
}