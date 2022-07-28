using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models;

public class BookingDto
{
    public Guid? BookingRef { get; set; }

    [Range(1, int.MaxValue)]
    public int RoomId { get; set; }

    [Range(1, int.MaxValue)]
    public int NoOfGuests { get; set; }

    [Required]
    public DateTime FromDate { get; set; }

    [Required]
    public DateTime ToDate { get; set; }
}