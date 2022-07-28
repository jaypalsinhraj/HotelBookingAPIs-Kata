using AutoFixture;
using AutoMapper;
using HotelBooking.APIs.Controllers;
using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBooking.APIs.Tests;

public class RoomControllerTests
{
    private readonly RoomController _roomController;
    private readonly Mock<IRoomData> _mockRoomData;

    public RoomControllerTests()
    {
        _mockRoomData = new Mock<IRoomData>();

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<Profiles.RoomProfile>());
        var mapper = new Mapper(mapperConfiguration);

        _mockRoomData.Setup(f => f.GetMaxCapacityForAnyRoomAsync(1)).ReturnsAsync(3);

        _roomController = new RoomController(_mockRoomData.Object, mapper);
    }

    [Fact]
    public async void GetAvailableRoomsForHotel_GetAction_WhenHotelIdIsLessThanOrZero_It_ReturnsBadRequestResult()
    {
        //Arrange
        var fakeHotelId = 0;
        var fakeNoOfGuests = 1;
        var fakeFromDateTime = DateTime.Now;
        var fakeToDateTime = DateTime.Now;

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async void GetAvailableRoomsForHotel_GetAction_WhenNoOfGuestsIsLessThanOrZero_It_ReturnsBadRequestResult()
    {
        //Arrange
        var fakeHotelId = 1;
        var fakeNoOfGuests = 0;
        var fakeFromDateTime = DateTime.Now;
        var fakeToDateTime = DateTime.Now;

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async void GetAvailableRoomsForHotel_GetAction_WhenDatesAreInPast_It_ReturnsBadRequestResult()
    {
        //Arrange
        var fakeHotelId = 1;
        var fakeNoOfGuests = 0;
        var fakeFromDateTime = DateTime.Now.AddDays(-3);
        var fakeToDateTime = DateTime.Now.AddDays(-2);

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetAvailableRoomsForHotel_GetAction_WhenToDateIsLessThanFromDate_It_ReturnsBadRequestResult()
    {
        //Arrange
        var fakeHotelId = 1;
        var fakeNoOfGuests = 1;
        var fakeFromDateTime = DateTime.Now.AddDays(1);
        var fakeToDateTime = DateTime.Now.AddDays(-2);

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("From date cannot be ahead of to date", badRequestResult.Value);
    }

    [Fact]
    public async Task GetAvailableRoomsForHotel_GetAction_WhenNoOfGuestsExceedsHotelsCapacity_It_ReturnsBadRequestResult()
    {
        //Arrange
        var fakeHotelId = 1;
        var fakeNoOfGuests = 4;
        var fakeFromDateTime = DateTime.Now.AddDays(1);
        var fakeToDateTime = DateTime.Now.AddDays(2);

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("Number of guests exceeds the hotels capacity", badRequestResult.Value);
    }


    [Fact]
    public async Task GetAvailableRoomsForHotel_GetAction_WhenNoVailableRoomsAreFound_It_ReturnsNotFoundResult()
    {
        //Arrange
        var fakeHotelId = 1;
        var fakeNoOfGuests = 1;
        var fakeFromDateTime = DateTime.Now.AddDays(1);
        var fakeToDateTime = DateTime.Now.AddDays(3);

        var emptyList = new List<Room>();
        _mockRoomData.Setup(m => m.GetAvailableRoomsForHotelAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(emptyList);

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);

    }

    [Fact]
    public async void GetAvailableRoomsForHotel_GetAction_WhenGivenValidParameters_It_ReturnsAvailableRooms()
    {
        //Arrange
        var fakeHotelId = 1;
        var fakeNoOfGuests = 1;
        var fakeFromDateTime = DateTime.Now.AddDays(1);
        var fakeToDateTime = DateTime.Now.AddDays(3);

        var listRooms = new List<Room>();
        for(var i = 0;i < 2; i++)
        {
            listRooms.Add(new Room("room 101 fake room")
            {
                Id = i,
                HotelId = 1,
                RoomTypeId = 1
            });
        }

        _mockRoomData.Setup(m => m.GetAvailableRoomsForHotelAsync(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime)).ReturnsAsync(listRooms);

        //Act
        var result = await _roomController.GetAvailableRoomsForHotel(fakeHotelId, fakeNoOfGuests, fakeFromDateTime, fakeToDateTime);

        //Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.RoomDto>>>(result);
        Assert.IsType<OkObjectResult>(actionResult.Result);
    }

}