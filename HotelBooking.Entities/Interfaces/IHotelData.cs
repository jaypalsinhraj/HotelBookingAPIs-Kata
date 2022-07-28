namespace HotelBooking.Entities.Interfaces;

public interface IHotelData
{
    Task<Hotel?> FindHotelByNameAsync(string name);
}