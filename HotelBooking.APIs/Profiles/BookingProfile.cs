using AutoMapper;

namespace HotelBooking.APIs.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Entities.Booking, Models.BookingDto>();
        CreateMap<Models.BookingDto, Entities.Booking>();
    }
}