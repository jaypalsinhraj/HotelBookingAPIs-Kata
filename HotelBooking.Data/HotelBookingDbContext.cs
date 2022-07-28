using HotelBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class HotelBookingDbContext : DbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel("Holiday Inn") { Id = 1 },
                new Hotel("Novotel") { Id = 2 },
                new Hotel("Premier Inn") { Id = 3});

            modelBuilder.Entity<RoomType>().HasData(
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
                });

            modelBuilder.Entity<Room>().HasData(
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
                });
        }
    }
}