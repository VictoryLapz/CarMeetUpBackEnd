using CarMeetUpApp.Data.Dto;
using CarMeetUpApp.Models;

namespace CarMeetUpApp.Mapper;

public static class CarMapper
{
    public static CarDto ToDto(this CarDto carItem) => new()
    {
        Make = carItem.Make,
        Model = carItem.Model,
        Year = carItem.Year,
        CarType = carItem.CarType,
        FuelType = carItem.FuelType,
        TransmissionType = carItem.TransmissionType,
        Color = carItem.Color,
    };

    public static CarDto CreateCar(this CarDto carDtoCreate) => new()
    {
        Make = carDtoCreate.Make,
        Model = carDtoCreate.Model,
        Year = carDtoCreate.Year,
        CarType = carDtoCreate.CarType,
        FuelType = carDtoCreate.FuelType,
        TransmissionType = carDtoCreate .TransmissionType,
        Color = carDtoCreate.Color,
    };

    public static CarDto UpdateCar(this CarDto carDtoUpdate) => new()
    {
        Make = carDtoUpdate.Make,
        Model = carDtoUpdate.Model,
        Year = carDtoUpdate.Year,
        CarType = carDtoUpdate.CarType,
        FuelType = carDtoUpdate.FuelType,
        TransmissionType = carDtoUpdate.TransmissionType,
        Color = carDtoUpdate.Color,
    };
}
