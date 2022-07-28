using AutoMapper;

namespace HotelBooking.APIs.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Entities.Room, Models.RoomDto>();
    }
}