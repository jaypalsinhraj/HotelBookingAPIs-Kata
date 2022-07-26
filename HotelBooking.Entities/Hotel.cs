using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Entities;

public class Hotel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Room> Rooms { get; set; } = new List<Room>();

}