using System.ComponentModel.DataAnnotations;
using BookingSystem.Booking.Api.Features.Events.Create;

namespace BookingSystem.Booking.Api.Features.Bookings.Create;

public enum BookingStatus { Created, Confirmed, Cancelled, Completed }

public class BookingEntity
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string CustomerEmail { get; set; } = null!;
    public string SeatNumber { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public BookingStatus Status { get; set; } = BookingStatus.Created;

    [Timestamp]
    public uint Version { get; set; }

    public EventEntity Event { get; set; } = null!;
}