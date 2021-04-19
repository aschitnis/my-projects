using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.models.json
{
    public class JsonCurrentWeatherModel
    {
        public JsonLatLonCoordinatesWeatherModel coord { get; set; }

        [JsonProperty("weather")]
        public List<JsonWeatherModel> weatherList { get; set; }
        public JsonMainWeatherModel main { get; set; }
        public JsonWindWeatherModel wind { get; set; }
        public JsonSystemWeatherModel sys { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string cod { get; set; }
        public string dt { get; set; }
        public JsonCloudsWeatherModel clouds { get; set; }
        public string timezone { get; set; }
        public string visibility { get; set; }

        [JsonProperty("base")]
        public string baseName { get; set; }
    }
    public class JsonSystemWeatherModel
    {
        [JsonProperty("type")]
        public string systyp { get; set; }
        public string id { get; set; }
        public string country { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }
    public class JsonCloudsWeatherModel
    {
        public string all { get; set; }
    }
    public class JsonLatLonCoordinatesWeatherModel
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
    public class JsonWeatherModel
    {
        public string id { get; set; }
        public string main { get; set; }
        public string description { get; set; }

    }
    public class JsonMainWeatherModel
    {
        public string temp { get; set; }
        public string feels_like { get; set; }
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string pressure { get; set; }
        public string humidity { get; set; }
    }
    public class JsonWindWeatherModel
    {
        public string speed { get; set; }
        public string deg { get; set; }
    }
}
