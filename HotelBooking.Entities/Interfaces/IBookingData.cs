namespace HotelBooking.Entities.Interfaces;

public interface IBookingData
{
    Task AddBooking(Booking booking);

    Task<Booking?> GetBookingByRefAsync(Guid bookingRef);

    Task<bool> ExistsBookingForRoomAsync(int roomId, DateTime fromDate, DateTime toDate);

    Task<bool> SaveChangesAsync();
}