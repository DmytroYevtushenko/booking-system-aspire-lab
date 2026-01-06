using FastEndpoints;

namespace BookingSystem.Booking.Api.Features.Bookings.Create;

public record CreateBookingRequest(Guid EventId, string CustomerEmail, string SeatNumber, decimal Price);
public record CreateBookingResponse(Guid Id, string Status, DateTime CreatedAt);

public class CreateBookingMapper : Mapper<CreateBookingRequest, CreateBookingResponse, BookingEntity>
{
    public override BookingEntity ToEntity(CreateBookingRequest r) => new()
    {
        Id = Guid.NewGuid(),
        EventId = r.EventId,
        CustomerEmail = r.CustomerEmail,
        SeatNumber = r.SeatNumber,
        Price = r.Price,
        Status = BookingStatus.Created
    };

    public override CreateBookingResponse FromEntity(BookingEntity e)
        => new(e.Id, e.Status.ToString(), e.CreatedAtUtc);
}