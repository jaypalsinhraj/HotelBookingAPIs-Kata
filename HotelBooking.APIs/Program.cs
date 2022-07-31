using HotelBooking.Data;
using HotelBooking.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-UK");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-UK")};
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelBookingDbContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:HotelBookingDbConnection"]));

builder.Services.AddScoped<IHotelData, HotelData>();
builder.Services.AddScoped<IRoomData, RoomData>();
builder.Services.AddScoped<IBookingData, BookingData>();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c  =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking API V1");
    c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
