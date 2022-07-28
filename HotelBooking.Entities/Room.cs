using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Entities;

public class Room
{
    public int Id { get; set; }

    public Hotel? Hotel { get; set; }
    public int HotelId { get; set; }

    public RoomType? RoomType { get; set; }
    public int RoomTypeId { get; set; }

    [Required]
    [MaxLength(128)]
    public string Title { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public Room(string title)
    {
        Title = title;
    }

}