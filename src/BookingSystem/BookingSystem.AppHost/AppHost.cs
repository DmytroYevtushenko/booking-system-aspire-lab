var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var bookingDb = postgres.AddDatabase("postgresDb");

builder.AddProject<Projects.BookingSystem_Booking_Api>("booking-api")
    .WithReference(bookingDb);

builder.Build().Run();