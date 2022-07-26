namespace HotelBooking.Models;

public class BookingDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}