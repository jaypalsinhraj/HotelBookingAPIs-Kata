using HotelBooking.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.APIs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataSeedingController : ControllerBase
{
    private readonly IDataSeeder _dataSeeder;

    public DataSeedingController(IDataSeeder dataSeeder)
    {
        _dataSeeder = dataSeeder;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteData()
    {
        await _dataSeeder.DataRemover();
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> CreateData()
    {
        await _dataSeeder.DataGenerator();
        return Ok();
    }
}