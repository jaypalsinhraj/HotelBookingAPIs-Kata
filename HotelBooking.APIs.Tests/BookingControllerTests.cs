using AutoMapper;
using HotelBooking.APIs.Controllers;
using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBooking.APIs.Tests;

public class BookingControllerTests
{
    private readonly BookingController _bookingController;
    private readonly Mock<IBookingData> _mockBookingData;
    private readonly Mock<IRoomData> _mockRoomData;
    private readonly Mapper mapper;

    public BookingControllerTests()
    {
        _mockBookingData = new Mock<IBookingData>();
        _mockRoomData = new Mock<IRoomData>();

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<Profiles.BookingProfile>());
        mapper = new Mapper(mapperConfiguration);

        _bookingController = new BookingController(_mockBookingData.Object, _mockRoomData.Object, mapper);
    }

    [Fact]
    public async Task GetBookingByBookingRef_GetAction_ReturnsOkObjectResult()
    {
        //Arrange
        var fakeBookingRef = Guid.NewGuid();

        var fakeBooking = new Booking()
        {
            BookingRef = fakeBookingRef,
            RoomId = 1,
            FromDate = DateTime.Now.AddDays(1),
            ToDate = DateTime.Now.AddDays(3)
        };

        _mockBookingData.Setup(f => f.GetBookingByRefAsync(fakeBookingRef)).ReturnsAsync(fakeBooking);

        //Act
        var result = await _bookingController.GetBookingByRef(fakeBookingRef.ToString());

        //Assert
        var actionResult = Assert.IsType<ActionResult<Models.BookingDto>>(result);
        Assert.IsType<OkObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetBookingByBookingRef_GetAction_WhenEmptyOrNullBookingRefGiven_It_ReturnsBadRequestResult()
    {
        //Act
        var result = await _bookingController.GetBookingByRef(" ");

        //Assert
        var actionResult = Assert.IsType<ActionResult<Models.BookingDto>>(result);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetBookingByBookingRef_GetAction_WhenNoBookingFound_It_ReturnsNotFoundResult()
    {
        //Act
        var result = await _bookingController.GetBookingByRef(Guid.NewGuid().ToString());

        //Assert
        var actionResult = Assert.IsType<ActionResult<Models.BookingDto>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task BookRoom_PostAction_WhenInvalidObjectPassed_It_ReturnsBadRequestResult()
    {
        //Arrange
        var fakeBookingDto = new BookingDto();

        //Act
        var result = await _bookingController.BookRoom(fakeBookingDto);

        //Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task BookRoom_PostAction_WhenRoomIsAlreadyBooked_It_ReturnsBadRequestWithMessageResult()
    {
        //Arrange
        var fakeBookingDto = new BookingDto()
        {
            RoomId = 1,
            NoOfGuests = 1,
            FromDate = DateTime.Now.AddDays(1),
            ToDate = DateTime.Now.AddDays(2)
        };

        _mockBookingData.Setup(b => b.ExistsBookingForRoomAsync(fakeBookingDto.RoomId, fakeBookingDto.FromDate, fakeBookingDto.ToDate)).ReturnsAsync(true);

        var badRequestErrorMessage = $"Room already booked between {fakeBookingDto.FromDate.ToString("dd-MMM-yyyy")} and {fakeBookingDto.ToDate.ToString("dd-MMM-yyyy")}";

        //Act
        var result = await _bookingController.BookRoom(fakeBookingDto);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(badRequestErrorMessage, badRequestResult.Value);
    }

    [Fact]
    public async Task BookRoom_PostAction_WhenRoomCapacityRequiredIsMoreThanAvailableForRoom_It_ReturnsBadRequestWithMessageResult()
    {
        //Arrange
        var fakeBookingDto = new BookingDto()
        {
            RoomId = 1,
            NoOfGuests = 3,
            FromDate = DateTime.Now.AddDays(1),
            ToDate = DateTime.Now.AddDays(2)
        };

        _mockRoomData.Setup(r => r.IsRoomCapacityValidAsync(fakeBookingDto.RoomId, fakeBookingDto.NoOfGuests)).ReturnsAsync(false);

        var badRequestErrorMessage = "Number of guests exceeds allowed capacity for the room";

        //Act
        var result = await _bookingController.BookRoom(fakeBookingDto);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(badRequestErrorMessage, badRequestResult.Value);
    }

    [Fact]
    public async Task BookRoom_PostAction_WhenBookingIsNotSaved_It_ReturnsInternalServerError()
    {
        //Arrange
        var fakeBookingDto = new BookingDto()
        {
            BookingRef = Guid.NewGuid(),
            RoomId = 1,
            NoOfGuests = 1,
            FromDate = DateTime.Now.AddDays(1),
            ToDate = DateTime.Now.AddDays(2)
        };

        var fakeBooking = mapper.Map<Booking>(fakeBookingDto);

        _mockBookingData.Setup(m => m.ExistsBookingForRoomAsync(fakeBooking.RoomId, fakeBooking.FromDate, fakeBooking.ToDate)).ReturnsAsync(false);

        _mockRoomData.Setup(m => m.IsRoomCapacityValidAsync(fakeBooking.RoomId, fakeBooking.NoOfGuests)).ReturnsAsync(true);

        _mockBookingData.Setup(m => m.AddBooking(fakeBooking));

        _mockBookingData.Setup(m => m.SaveChangesAsync()).ReturnsAsync(false);

        //Act
        var result = await _bookingController.BookRoom(fakeBookingDto);

        //Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result);

        Assert.Equal("Booking is not confirmed, please try again", internalServerErrorResult.Value);
    }

    [Fact]
    public async Task BookRoom_PostAction_WhenValidBookingDtoGiven_It_SavesTheBooking()
    {
        //Arrange
        var fakeBookingDto = new BookingDto()
        {
            BookingRef = Guid.NewGuid(),
            RoomId = 1,
            NoOfGuests = 1,
            FromDate = DateTime.Now.AddDays(1),
            ToDate = DateTime.Now.AddDays(2)
        };

        var fakeBooking = mapper.Map<Booking>(fakeBookingDto);

        _mockBookingData.Setup(m => m.ExistsBookingForRoomAsync(fakeBooking.RoomId, fakeBooking.FromDate, fakeBooking.ToDate)).ReturnsAsync(false);

        _mockRoomData.Setup(m => m.IsRoomCapacityValidAsync(fakeBooking.RoomId, fakeBooking.NoOfGuests)).ReturnsAsync(true);

        _mockBookingData.Setup(m => m.AddBooking(fakeBooking));

        _mockBookingData.Setup(m => m.SaveChangesAsync()).ReturnsAsync(true);

        //Act
        var result = await _bookingController.BookRoom(fakeBookingDto);
        
        //Assert
        var redirectoToActionResult = Assert.IsType<CreatedAtRouteResult>(result);
        var model = Assert.IsAssignableFrom<BookingDto>(redirectoToActionResult.Value);

        Assert.Equal(fakeBookingDto.BookingRef, model.BookingRef);

    }
}