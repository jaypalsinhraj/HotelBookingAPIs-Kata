namespace HotelBooking.Entities.Interfaces;

public interface IGuestData
{
    Task<Guest> GetGuestByIdAsync(int id);

    Task AddGuest(Guest guest);
}