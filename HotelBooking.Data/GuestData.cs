using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;

namespace HotelBooking.Data
{
    public class GuestData : BaseData, IGuestData
    {
        public GuestData(HotelBookingDbContext context) : base(context)
        {
        }

        public Task AddGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public Task<Guest> GetGuestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}