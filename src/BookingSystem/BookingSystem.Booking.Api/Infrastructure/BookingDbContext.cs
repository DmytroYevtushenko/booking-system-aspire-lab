using BookingSystem.Booking.Api.Features.Bookings.Create;
using BookingSystem.Booking.Api.Features.Events.Create;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Booking.Api.Infrastructure;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events => Set<EventEntity>();
    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingEntity>(b =>
        {
            b.Property(x => x.Price).HasPrecision(18, 2);
            b.Property(x => x.CustomerEmail).HasMaxLength(256);
            b.Property(x => x.SeatNumber).HasMaxLength(50);

            // Prohibit double booking for the same event and seat
            b.HasIndex(x => new { x.EventId, x.SeatNumber }).IsUnique();

            // Optimistic concurrency control
            b.Property(x => x.Version).IsRowVersion();
        });

        modelBuilder.Entity<EventEntity>(e =>
        {
            e.Property(x => x.Title).HasMaxLength(200);
            e.Property(x => x.Description).HasColumnType("text");
            e.HasMany(x => x.Bookings).WithOne(x => x.Event).HasForeignKey(x => x.EventId);
            e.HasQueryFilter(x => !x.IsDeleted);
        });
    }
}