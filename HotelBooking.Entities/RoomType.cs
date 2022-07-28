using HotelBooking.Entities.CusotmValidators;
using HotelBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Entities;

public class RoomType
{
    public int Id { get; set; }
    public RoomTypeEnum Type { get; set; }
    
    [Required]
    [RoomCapacityValidation]
    public int Capacity { get; set; }
}


