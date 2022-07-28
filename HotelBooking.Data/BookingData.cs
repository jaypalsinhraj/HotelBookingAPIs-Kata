using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class BookingData : BaseData, IBookingData
    {
        public BookingData(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task AddBooking(Booking booking)
        {
            await _context.AddAsync(booking);
        }

        public async Task<bool> ExistsBookingForRoomAsync(int roomId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Bookings.AnyAsync(b => b.RoomId == roomId && b.FromDate >= fromDate && b.ToDate <= toDate);
        }

        public async Task<Booking?> GetBookingByRefAsync(Guid bookingRef)
        {
            return (await _context.Bookings.SingleOrDefaultAsync(b => b.BookingRef == bookingRef))!;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 1);
        }


    }
}