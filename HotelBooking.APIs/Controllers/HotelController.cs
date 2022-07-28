using AutoMapper;
using HotelBooking.Entities.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.APIs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelData _hotelData;
    private readonly IMapper _mapper;

    public HotelController(IHotelData hotelData, IMapper mapper)
    {
        _hotelData = hotelData;
        _mapper = mapper;
    }

    [HttpGet("findByName/{name}")]
    public async Task<ActionResult<HotelDto>> GetHotelByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest();

        var hotel = await _hotelData.FindHotelByNameAsync(name);

        if (hotel == null)
            return NotFound();

        return Ok(_mapper.Map<HotelDto>(hotel));
    }



}