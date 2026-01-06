using FastEndpoints;
using FluentValidation;

namespace BookingSystem.Booking.Api.Features.Bookings.Create;

public class CreateBookingValidator : Validator<CreateBookingRequest>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.CustomerEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.SeatNumber).NotEmpty();
        RuleFor(x => x.EventId).NotEmpty();
    }
}