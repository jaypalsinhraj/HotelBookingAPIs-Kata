using HotelBooking.Models;

namespace HotelBooking.Entities;

public class RoomType
{
    public int Id { get; set; }
    public RoomTypeEnum Type { get; set; }
    public int Capacity { get; set; }
}