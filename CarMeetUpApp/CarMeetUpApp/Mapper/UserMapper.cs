using CarMeetUpApp.Data.Dto;
using CarMeetUpApp.Models;
using Microsoft.Identity.Client;
namespace CarMeetUpApp.Mapper;

public static class UserMapper
{
    public static UserDto ToDto(this UserDto userItem) => new()
    {
        FirstName = userItem.FirstName,
        LastName = userItem.LastName,
        Email = userItem.Email,
        Bio = userItem.Bio,
        CarInterests = userItem.CarInterests,
    };

    public static UserDto CreateDto(this UserDto userDtoCreate) => new()
    {
        FirstName = userDtoCreate.FirstName,
        LastName = userDtoCreate.LastName, 
        Email = userDtoCreate.Email,
        Bio = userDtoCreate.Bio,
        CarInterests= userDtoCreate.CarInterests, 
    };

    public static UserDto UpdateDto(this UserDto userDtoUpdate) => new()
    {
        FirstName = userDtoUpdate.FirstName,
        LastName = userDtoUpdate.LastName,
        Email = userDtoUpdate.Email,
        Bio = userDtoUpdate.Bio,
        CarInterests = userDtoUpdate.CarInterests,
    };

}
