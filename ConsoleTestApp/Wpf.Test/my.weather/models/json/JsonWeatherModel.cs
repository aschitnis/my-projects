using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.models.json
{
    //
    public class JsonWeatherModel
    {
        [JsonProperty("name")]
        public string City { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("temp")]
        public double CurrentTemperature { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLikeTemperature { get; set; }
        [JsonProperty("main.temp_min")]
        public double MinTemperature { get; set; }
        [JsonProperty("main.temp_max")]
        public double MaxTemperature { get; set; }
        [JsonProperty("timezone")]
        public double TimeZoneInSeconds { get; set; }
        [JsonProperty("sys.sunrise")]
        public double SunriseUtcTimeInSeconds { get; set; }
        [JsonProperty("sys.sunset")]
        public double SunsetUtcTimeInSeconds { get; set; }
        public JsonWeatherModel() { }
    }
}
