using AutoMapper;
using HotelBooking.APIs.Controllers;
using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBooking.APIs.Tests;

public class HotelControllerTests
{
    private readonly HotelController _hotelController;
    private readonly Mock<IHotelData> _mockHotelData;

    public HotelControllerTests()
    {
        _mockHotelData = new Mock<IHotelData>();

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<Profiles.HotelProfile>());
        var mapper = new Mapper(mapperConfiguration);

        _hotelController = new HotelController(_mockHotelData.Object, mapper);
    }

    [Fact]
    public async Task GetHotelByName_GetAction_ReturnsOkObjectResult()
    {
        //Arrange
        var fakeHotel = new Hotel("fake hotel")
        {
            Id = 1
        };

        _mockHotelData.Setup(f => f.FindHotelByNameAsync("fake hotel")).ReturnsAsync(fakeHotel);

        //Act
        var result = await _hotelController.GetHotelByName("fake hotel");

        //Assert
        var actionResult = Assert.IsType<ActionResult<Models.HotelDto>>(result);
        Assert.IsType<OkObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetHotelByName_GetAction_WhenEmptyNameStringIsPassed_It_ReturnsBadRequestResult()
    {
        //Act
        var result = await _hotelController.GetHotelByName(" ");

        //Assert
        var actionResult = Assert.IsType<ActionResult<Models.HotelDto>>(result);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetHotelByName_GetAction_WhenNoHotelsFoundWithAName_It_ReturnsNotFoundResult()
    {
        //Arrange
        var fakeHotel = new Hotel("fake hotel")
        {
            Id = 1
        };

        _mockHotelData.Setup(f => f.FindHotelByNameAsync("fake hotel")).ReturnsAsync(fakeHotel);

        //Act
        var result = await _hotelController.GetHotelByName("Random name");

        //Assert
        var actionResult = Assert.IsType<ActionResult<Models.HotelDto>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
}