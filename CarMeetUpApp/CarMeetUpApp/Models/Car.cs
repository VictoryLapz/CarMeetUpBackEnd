using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMeetUpApp.Models;

public class Car 
{
    [Key] 
    public int Id { get; set; } 

    [JsonProperty("city_mpg")]
    public int CityMpg { get; set; }

    [JsonProperty("class")]
    public required string Class { get; set; }

    [JsonProperty("combination_mpg")]
    public int CombinationMpg { get; set; }

    [JsonProperty("cylinders")]
    public int Cylinders { get; set; }

    [JsonProperty("displacement")]
    public double Displacement { get; set; }

    [JsonProperty("drive")]
    public required string Drive { get; set; }

    [JsonProperty("fuel_type")]
    public required string FuelType { get; set; }

    [JsonProperty("highway_mpg")]
    public int HighwayMpg { get; set; }

    [JsonProperty("make")]
    public required string Make { get; set; }

    [JsonProperty("model")]
    public required string Model { get; set; }

    [JsonProperty("transmission")]
    public required string Transmission { get; set; }

    [JsonProperty("year")]
    public required int Year { get; set; }
}


