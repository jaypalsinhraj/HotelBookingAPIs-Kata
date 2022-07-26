using HotelBooking.Entities;

namespace HotelBooking.Data.Tests
{
    public class HoteBookingDataFixture : TestBase
    {
        public HoteBookingDataFixture()
        {
            dbContext.Hotels.Add(new Hotel { Id = 1, Name = "HolidayInn" });

            dbContext.SaveChanges();
        }
    }
}