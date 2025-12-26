var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BookingSystem_Booking_Api>("booking-api");

builder.Build().Run();