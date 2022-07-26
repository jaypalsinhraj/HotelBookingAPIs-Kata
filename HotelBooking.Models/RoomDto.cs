namespace HotelBooking.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}