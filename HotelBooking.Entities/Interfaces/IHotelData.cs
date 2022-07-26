namespace HotelBooking.Entities.Interfaces;

public interface IHotelData
{
    Task<Hotel?> GetHotelByIdAsync(int id);

    Task<Hotel?> FindHotelByNameAsync(string name);

    Task AddHotelAsync(Hotel hotel);

    Task<bool> SaveChangesAsync();
}