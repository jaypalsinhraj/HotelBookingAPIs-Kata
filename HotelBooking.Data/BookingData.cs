using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;

namespace HotelBooking.Data
{
    public class BookingData : BaseData, IBookingData
    {
        public BookingData(HotelBookingDbContext context) : base(context)
        {
        }

        public Task<bool> ExistsBookingForRoomAsync(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingByRefAsync(Guid bookingRef)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetBookingsForRoomAsync(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetBookingsForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}