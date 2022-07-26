using HotelBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data.Tests;

public class TestBase : IDisposable
{
    public readonly DbContextOptions<HotelBookingDbContext> dbContextOptions;
    public readonly HotelBookingDbContext dbContext;

    public TestBase()
    {
        dbContextOptions = new DbContextOptionsBuilder<HotelBookingDbContext>()
            .UseInMemoryDatabase(databaseName: "HotelBooking").Options;

        dbContext = new HotelBookingDbContext(dbContextOptions);
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}