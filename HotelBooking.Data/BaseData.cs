namespace HotelBooking.Data;

public class BaseData
{
    protected readonly HotelBookingDbContext _context;

    public BaseData(HotelBookingDbContext context)
    {
        _context = context;
    }
}