namespace HotelBooking.Entities.Interfaces;

public interface IDataSeeder
{
    Task DataRemover();

    Task DataGenerator();
}