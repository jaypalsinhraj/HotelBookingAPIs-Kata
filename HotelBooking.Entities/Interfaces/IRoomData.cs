namespace HotelBooking.Entities.Interfaces;

public interface IRoomData
{
    Task<IEnumerable<Room>> GetAvailableRoomsForHotelAsync(int hotelId, int noOfGuests, DateTime fromDate, DateTime toDate);

    Task<int> GetMaxCapacityForAnyRoomAsync(int hotelId);

    Task<bool> IsRoomCapacityValidAsync(int roomId, int requiredCapacity);
}