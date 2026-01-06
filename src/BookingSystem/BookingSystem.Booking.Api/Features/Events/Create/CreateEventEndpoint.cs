using FastEndpoints;
using BookingSystem.Booking.Api.Features.Bookings.Create;
using BookingSystem.Booking.Api.Infrastructure;

namespace BookingSystem.Booking.Api.Features.Events.Create;

public record CreateEventRequest(string Title, string Description, DateTime Date);
public record CreateEventResponse(Guid Id, string Title, string Description, DateTime Date, bool IsActive);

public class CreateEventEndpoint(BookingDbContext db) : Endpoint<CreateEventRequest, CreateEventResponse>
{
    public override void Configure()
    {
        Post("/api/events");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateEventRequest req, CancellationToken ct)
    {
        var entity = new EventEntity
        {
            Id = Guid.NewGuid(),
            Title = req.Title,
            Description = req.Description,
            Date = req.Date,
            IsActive = true
        };

        db.Events.Add(entity);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new CreateEventResponse(entity.Id, entity.Title, entity.Description, entity.Date, entity.IsActive), ct);
    }
}