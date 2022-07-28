using AutoMapper;
using HotelBooking.Entities.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.APIs.Controllers
{
    [ApiController]
    [Route("api/hotel/{hotelId}/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomData _roomData;
        private readonly IMapper _mapper;

        public RoomController(IRoomData roomData, IMapper mapper)
        {
            _roomData = roomData;
            _mapper = mapper;
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAvailableRoomsForHotel(int hotelId, int noOfGuests, DateTime fromDate, DateTime toDate)
        {
            if (hotelId <= 0 || noOfGuests <= 0 || fromDate == DateTime.MinValue || toDate == DateTime.MinValue)
                return BadRequest();

            if (fromDate > toDate)
                return BadRequest("From date cannot be ahead of to date");

            var maxCapacityOfAnyRoomAtHotel = await _roomData.GetMaxCapacityForAnyRoomAsync(hotelId);
            if (noOfGuests > maxCapacityOfAnyRoomAtHotel)
                return BadRequest("Number of guests exceeds the hotels capacity");

            var availableRooms = await _roomData.GetAvailableRoomsForHotelAsync(hotelId, noOfGuests, fromDate, toDate);

            if (!availableRooms.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<RoomDto>>(availableRooms));
        }
    }
}