using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Entities;

public class Guest
{
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string FullName { get; set; } = string.Empty;
}