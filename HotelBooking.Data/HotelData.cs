using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class HotelData : BaseData, IHotelData
    {
        public HotelData(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<Hotel?> FindHotelByNameAsync(string name)
        {
            return (await _context.Hotels.Include(h => h.Rooms).SingleOrDefaultAsync(h => h.Name == name))!;
        }
    }
}