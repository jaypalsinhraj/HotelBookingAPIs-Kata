using AutoMapper;
using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.APIs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingData _bookingData;
    private readonly IRoomData _roomData;
    private readonly IMapper _mapper;

    public BookingController(IBookingData bookingData, IRoomData roomData, IMapper mapper)
    {
        _bookingData = bookingData;
        _roomData = roomData;
        _mapper = mapper;
    }

    [HttpGet("{bookingRef}", Name = "GetBookingByRef")]
    public async Task<ActionResult<BookingDto>> GetBookingByRef(string bookingRef)
    {
        if (string.IsNullOrWhiteSpace(bookingRef))
            return BadRequest();

        var booking = await _bookingData.GetBookingByRefAsync(Guid.Parse(bookingRef));
        if (booking == null)
            return NotFound();

        return Ok(_mapper.Map<BookingDto>(booking));
    }

    [HttpPost]
    public async Task<IActionResult> BookRoom([FromBody]BookingDto value)
    {
        if (value.RoomId <= 0 || value.NoOfGuests <= 0 || value.FromDate <= DateTime.MinValue || value.ToDate <= DateTime.MinValue)
            return BadRequest();

        var isRoomBooked = await _bookingData.ExistsBookingForRoomAsync(value.RoomId, value.FromDate, value.ToDate);
        if (isRoomBooked)
            return BadRequest($"Room already booked between { value.FromDate.ToString("dd-MMM-yyyy") } and { value.ToDate.ToString("dd-MMM-yyyy") }");

        var isRequiredCapacityValid = await _roomData.IsRoomCapacityValidAsync(value.RoomId, value.NoOfGuests);
        if (!isRequiredCapacityValid)
            return BadRequest("Number of guests exceeds allowed capacity for the room");

        var booking = _mapper.Map<Booking>(value);
        await _bookingData.AddBooking(booking);
        var result = await _bookingData.SaveChangesAsync();

        if (!result)
            return StatusCode(500, "Booking is not confirmed, please try again");

        var bookingReturned = _mapper.Map<BookingDto>(booking);

        return CreatedAtRoute("GetBookingByRef", new { bookingRef = bookingReturned.BookingRef }, bookingReturned);
    }
}