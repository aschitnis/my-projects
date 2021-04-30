using MyProjects.WeatherData.Wpf.WeatherDataUI.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProjects.WeatherData.Wpf.WeatherDataUI.classes
{
    public class JsonHelperConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(JsonWeatherApi));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            JsonWeatherApi weather = jo.ToObject<JsonWeatherApi>();
            weather.Latitude = (double)jo.SelectToken("coord.lat");
            weather.Longitude = (double)jo.SelectToken("coord.lon");
            weather.Description = jo.SelectToken("weather.[0].description")?.ToString();
            weather.CurrentTemperature = (double)jo.SelectToken("main.temp");
            weather.FeelsLikeTemperature = (double)jo.SelectToken("main.feels_like");
            return weather;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
