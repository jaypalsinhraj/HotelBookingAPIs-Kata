using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class RoomData : BaseData, IRoomData
    {
        public RoomData(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsForHotelAsync(int hotelId, int noOfGuests, DateTime fromDate, DateTime toDate)
        {
            var bookingsBetweenDates = _context.Bookings.Where(b => (fromDate.Date >= b.FromDate.Date && fromDate.Date <= b.ToDate.Date));

            var rooms = _context.Rooms.Include(t => t.RoomType).Where(r => r.HotelId == hotelId && r.RoomType != null && r.RoomType.Capacity >= noOfGuests);

            return await rooms.Where(r => !bookingsBetweenDates.Any(b => b.RoomId == r.Id)).ToListAsync();
        }

        public async Task<int> GetMaxCapacityForAnyRoomAsync(int hotelId)
        {
            return await _context.Rooms.Include(r => r.RoomType)
                                       .Where(r => r.HotelId == hotelId && r.RoomType != null)
                                       .MaxAsync(m => m.RoomType.Capacity);
        }

        public async Task<bool> IsRoomCapacityValidAsync(int roomId, int requiredCapacity)
        {
            return await _context.Rooms.Include(r => r.RoomType).AnyAsync(r => r.Id == roomId && r.RoomType != null && requiredCapacity <= r.RoomType.Capacity);
        }
    }
}