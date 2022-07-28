using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Entities;

public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid BookingRef { get; set; }

    public Room? Room { get; set; }
    public int RoomId { get; set; }

    [Required]
    public int NoOfGuests { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}