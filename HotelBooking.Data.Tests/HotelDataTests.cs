using HotelBooking.Entities;

namespace HotelBooking.Data.Tests
{
    public class HotelDataTests : TestBase, IClassFixture<HoteBookingDataFixture>
    {
        [Fact]
        public async Task WhenGetHotelByIdIsCalledThenItShouldReturnTheHotel()
        {
            var _hotelData = new HotelData(dbContext);

            var hotel = await _hotelData.GetHotelByIdAsync(1);

            Assert.True(hotel?.Name == "HolidayInn");
        }

        [Fact]
        public async Task WhenFindHotelByNameAsyncIsCalledThenItShouldReturnTheHotel()
        {
            var _hotelData = new HotelData(dbContext);

            var hotel = await _hotelData.FindHotelByNameAsync("HolidayInn");

            Assert.True(hotel?.Id == 1);
        }

        [Fact]
        public async Task WhenAddHotelIsCalledItShouldInsertANewHotel()
        {
            var _hotelData = new HotelData(dbContext);

            var hotel = new Hotel()
            {
                Name = "Novotel"
            };

            await _hotelData.AddHotelAsync(hotel);
            await _hotelData.SaveChangesAsync();

            Assert.True(hotel.Id > 0);
        }
    }
}