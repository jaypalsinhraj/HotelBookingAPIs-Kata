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

        public async Task AddHotelAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
        }

        public async Task<Hotel?> FindHotelByNameAsync(string name)
        {
            return (await _context.Hotels.SingleOrDefaultAsync(h => h.Name == name))!;
        }

        public async Task<Hotel?> GetHotelByIdAsync(int id)
        {
            return (await _context.Hotels.SingleOrDefaultAsync(h => h.Id == id))!;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 1);
        }
    }
}