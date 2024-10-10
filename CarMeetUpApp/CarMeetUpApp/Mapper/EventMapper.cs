﻿using CarMeetUpApp.Models;
using CarMeetUpApp.Data.Dto;
using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Mapper;

public static class EventMapper
{
    public static EventDto ToDto(this Event eventItem) => new()
    {
        Title = eventItem.Title,
        Location = eventItem.Location,
        Description = eventItem.Description,
        Date = eventItem.Date,
        Capacity = eventItem.Capacity,
    };

    public static Event FromCreateDto(this EventDto createEventDto) => new()
    {
        Title = createEventDto.Title,
        Location = createEventDto.Location,
        Description = createEventDto.Description,
        Date = createEventDto.Date,
        Capacity = createEventDto.Capacity,
    };

    public static Event FromUpdateDto(this EventDto updateEventDto) => new()
    {
        Title = updateEventDto.Title,
        Location = updateEventDto.Location,
        Description = updateEventDto.Description,
        Date = updateEventDto.Date,
        Capacity = updateEventDto.Capacity
    };
}

