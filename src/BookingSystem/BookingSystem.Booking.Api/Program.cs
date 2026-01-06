using BookingSystem.Booking.Api.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.AddNpgsqlDbContext<BookingDbContext>("BookingDb");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
    await db.Database.MigrateAsync();
}

app.UseFastEndpoints();

app.UseSwaggerGen();

app.MapDefaultEndpoints();

app.Run();