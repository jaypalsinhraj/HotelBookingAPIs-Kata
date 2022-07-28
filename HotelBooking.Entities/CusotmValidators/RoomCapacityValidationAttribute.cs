using HotelBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Entities.CusotmValidators;

public class RoomCapacityValidationAttribute : ValidationAttribute
{
    public string GetErrorMessage(string roomType, string capacity) =>
        $"Capacity cannot be more than {capacity} for a {roomType}, use a different room type to accommodate required capacity";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var roomType = (RoomType)validationContext.ObjectInstance;
        var capacity = Convert.ToInt32(value);

        if (roomType.Type == RoomTypeEnum.Single && capacity > 1)
            return new ValidationResult(GetErrorMessage("single room", "1"));

        if (roomType.Type == RoomTypeEnum.Double && capacity > 2)
            return new ValidationResult(GetErrorMessage("double room", "2"));

        if (roomType.Type == RoomTypeEnum.Delux && capacity > 4)
            return new ValidationResult(GetErrorMessage("delux room", "4"));

        return ValidationResult.Success;
    }
}