using BookingSystem.Booking.Api.Features.Bookings.Create;

namespace BookingSystem.Booking.Api.Features.Events.Create;

public class EventEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public ICollection<BookingEntity> Bookings { get; set; } = [];
}