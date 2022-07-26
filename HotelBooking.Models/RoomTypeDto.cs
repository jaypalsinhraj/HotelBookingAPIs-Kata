namespace HotelBooking.Models
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public RoomTypeEnum Type { get; set; }
        public int Capacity { get; set; }
    }
}