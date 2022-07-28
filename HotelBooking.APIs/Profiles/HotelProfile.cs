using AutoMapper;

namespace HotelBooking.APIs.Profiles;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Entities.Hotel, Models.HotelDto>();
    }
}