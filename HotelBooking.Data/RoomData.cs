using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;

namespace HotelBooking.Data
{
    public class RoomData : BaseData, IRoomData
    {
        public RoomData(HotelBookingDbContext context) : base(context)
        {
        }

        public Task AddRoomAsync(int hotelId, Room room)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetRoomByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetRoomsForHotelAsync(int hotelId)
        {
            throw new NotImplementedException();
        }
    }
}