using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BryanPorter.SlackCmd.Models
{
    public class WeatherResponse
    {
        [JsonProperty("coord")]
        public WeatherCoordinate Coordinate { get; set; }

        [JsonProperty("weather")]
        public IEnumerable<WeatherConditionCode> WeatcherCondition { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public WeatherDescriptor Weather { get; set; }

        [JsonProperty("wind")]
        public WindDescriptor Wind { get; set; }

        [JsonProperty("clouds")]
        public CloudDescriptor Clouds { get; set; }

        [JsonProperty("rain")]
        public PrecipDescriptor RainDescriptor { get; set; }

        [JsonProperty("snow")]
        public PrecipDescriptor SnowDescriptor { get; set; }

        [JsonProperty("dt")]
        public int TimeOfCalculation { get; set; }

        [JsonProperty("sys")]
        public WeatherSystem SystemInfo { get; set; }

        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("cod")]
        public int InternalParam { get; set; }
    }
    
    public class WeatherCoordinate
    {
        [JsonProperty("lon")]
        public float Longitude { get; set; }

        [JsonProperty("lat")]
        public float Latitude { get; set; }
    }

    public class WeatherConditionCode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string IconId { get; set; }
    }

    public class WeatherDescriptor
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("temp_min")]
        public float CurrentMinimumTemperature { get; set; }

        [JsonProperty("temp_max")]
        public float CurrentMaximumTemperature { get; set; }

        [JsonProperty("sea_level")]
        public int SeaLevelPressure { get; set; }

        [JsonProperty("grnd_level")]
        public int GroundLevelPressure { get; set; }
    }

    public class WindDescriptor
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("deg")]
        public float Degrees { get; set; }
    }

    public class CloudDescriptor
    {
        [JsonProperty("all")]
        public int CloudinessPercentage { get; set; }
    }

    public class PrecipDescriptor
    {
        [JsonProperty("3h")]
        public int LastThreeHours { get; set; }
    }

    public class WeatherSystem
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("message")]
        public float Messsage { get; set; }

        [JsonProperty("country")]
        public string CountryCode { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }
}
