using HotelBooking.Entities;
using HotelBooking.Entities.Interfaces;

namespace HotelBooking.Data;

public class DataSeeder : IDataSeeder
{
    private readonly HotelBookingDbContext _hotelBookingDbContext;

    public DataSeeder(HotelBookingDbContext hotelBookingDbContext)
    {
        _hotelBookingDbContext = hotelBookingDbContext;
    }

    public async Task DataGenerator()
    {
        await _hotelBookingDbContext.Database.EnsureCreatedAsync();

        if (!_hotelBookingDbContext.Hotels.Any())
        {
            var hotels = new Hotel[]
            {
                new Hotel("Holiday Inn") { Id = 1 },
                new Hotel("Novotel") { Id = 2 },
                new Hotel("Premier Inn") { Id = 3}
            };
            await _hotelBookingDbContext.Hotels.AddRangeAsync(hotels);
        }

        if (!_hotelBookingDbContext.RoomTypes.Any())
        {
            var roomTypes = new RoomType[]
            {
                new RoomType()
                {
                    Id = 1,
                    Type = Models.RoomTypeEnum.Single,
                    Capacity = 1
                },
                new RoomType()
                {
                    Id = 2,
                    Type = Models.RoomTypeEnum.Double,
                    Capacity = 2,
                },
                new RoomType()
                {
                    Id = 3,
                    Type = Models.RoomTypeEnum.Delux,
                    Capacity = 3
                }
            };
            await _hotelBookingDbContext.RoomTypes.AddRangeAsync(roomTypes);
        }

        if (!_hotelBookingDbContext.Rooms.Any())
        {
            var rooms = new Room[]
            {
                new Room("Room 101")
                {
                    Id = 1,
                    HotelId = 1,
                    RoomTypeId = 1
                },
                new Room("Room 102")
                {
                    Id = 2,
                    HotelId = 1,
                    RoomTypeId = 2
                },
                new Room("Room 103")
                {
                    Id = 3,
                    HotelId = 1,
                    RoomTypeId = 3
                },
                new Room("Room 201")
                {
                    Id = 4,
                    HotelId = 1,
                    RoomTypeId = 1
                },
                new Room("Room 202")
                {
                    Id = 5,
                    HotelId = 1,
                    RoomTypeId = 2
                },
                new Room("Room 203")
                {
                    Id = 6,
                    HotelId = 1,
                    RoomTypeId = 3
                },
                new Room("Room 101")
                {
                    Id = 7,
                    HotelId = 2,
                    RoomTypeId = 1
                },
                new Room("Room 102")
                {
                    Id = 8,
                    HotelId = 2,
                    RoomTypeId = 2
                },
                new Room("Room 103")
                {
                    Id = 9,
                    HotelId = 2,
                    RoomTypeId = 3
                },
                new Room("Room 201")
                {
                    Id = 10,
                    HotelId = 2,
                    RoomTypeId = 1
                },
                new Room("Room 202")
                {
                    Id = 11,
                    HotelId = 2,
                    RoomTypeId = 2
                },
                new Room("Room 203")
                {
                    Id = 12,
                    HotelId = 2,
                    RoomTypeId = 3
                },
                new Room("Room 101")
                {
                    Id = 13,
                    HotelId = 3,
                    RoomTypeId = 1
                },
                new Room("Room 102")
                {
                    Id = 14,
                    HotelId = 3,
                    RoomTypeId = 2
                },
                new Room("Room 103")
                {
                    Id = 15,
                    HotelId = 3,
                    RoomTypeId = 3
                },
                new Room("Room 201")
                {
                    Id = 16,
                    HotelId = 3,
                    RoomTypeId = 1
                },
                new Room("Room 202")
                {
                    Id = 17,
                    HotelId = 3,
                    RoomTypeId = 2
                },
                new Room("Room 203")
                {
                    Id = 18,
                    HotelId = 3,
                    RoomTypeId = 3
                }
            };

            await _hotelBookingDbContext.Rooms.AddRangeAsync(rooms);
        }

        await _hotelBookingDbContext.SaveChangesAsync();
    }

    public async Task DataRemover()
    {
        await _hotelBookingDbContext.Database.EnsureDeletedAsync();
    }
}