namespace HotelBooking.Models
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    }
}