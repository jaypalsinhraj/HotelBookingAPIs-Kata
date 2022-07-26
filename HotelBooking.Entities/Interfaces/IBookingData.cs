namespace HotelBooking.Entities.Interfaces;

public interface IBookingData
{
    Task<Booking> GetBookingByRefAsync(Guid bookingRef);

    Task<IEnumerable<Booking>> GetBookingsForUserAsync(int userId);

    Task<IEnumerable<Booking>> GetBookingsForRoomAsync(int roomId);

    Task<bool> ExistsBookingForRoomAsync(int roomId);
}