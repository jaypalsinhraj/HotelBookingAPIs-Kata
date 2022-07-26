namespace HotelBooking.Entities.Interfaces;

public interface IRoomData
{
    Task<Room> GetRoomByIdAsync(int id);

    Task<IEnumerable<Room>> GetRoomsForHotelAsync(int hotelId);

    Task AddRoomAsync(int hotelId, Room room);
}